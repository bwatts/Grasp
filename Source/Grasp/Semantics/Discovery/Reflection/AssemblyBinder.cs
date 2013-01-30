using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp.Checks;
using Grasp.Work;

namespace Grasp.Semantics.Discovery.Reflection
{
	public static class AssemblyBinder
	{
		public static ReflectionBinding BindDomainModel(this Assembly assembly, FullName name, bool includeGrasp = true, IAssemblyBindingLog log = null)
		{
			Contract.Requires(name != null);
			Contract.Requires(assembly != null);

			return Params.Of(assembly).BindDomainModel(name, includeGrasp, log);
		}

		public static ReflectionBinding BindDomainModel(this IEnumerable<Assembly> assemblies, FullName name, bool includeGrasp = true, IAssemblyBindingLog log = null)
		{
			Contract.Requires(name != null);
			Contract.Requires(assemblies != null);

			return new ReflectionBinding(name, BindNamespace(assemblies, includeGrasp, log));
		}

		private static IEnumerable<NamespaceBinding> BindNamespace(IEnumerable<Assembly> assemblies, bool includeGrasp, IAssemblyBindingLog log)
		{
			assemblies = GetEffectiveAssemblies(assemblies, includeGrasp);

			return new AssemblyBinderContext(log).BindNamespaces(assemblies);
		}

		private static IEnumerable<Assembly> GetEffectiveAssemblies(IEnumerable<Assembly> assemblies, bool includeGrasp)
		{
			var grasp = Params.Of(Assembly.GetExecutingAssembly());

			assemblies = includeGrasp ? assemblies.Concat(grasp) : assemblies.Except(grasp);

			return assemblies.Distinct();
		}

		private sealed class AssemblyBinderContext
		{
			private readonly IAssemblyBindingLog _log;
			private readonly Func<Type, EnumBinding> _bindEnum;

			internal AssemblyBinderContext(IAssemblyBindingLog log)
			{
				_log = log ?? new ConsoleBindingLog();

				_bindEnum = type => new EnumBinding(type);

				_bindEnum = _bindEnum.Cached();
			}

			internal IEnumerable<NamespaceBinding> BindNamespaces(IEnumerable<Assembly> assemblies)
			{
				return
					from assembly in assemblies
					from type in assembly.GetTypes()
					where !IsIgnored(type)
					group type by type.Namespace into typesByNamespace
					orderby typesByNamespace.Key
					select new NamespaceBinding(typesByNamespace.Key, BindNamespaceParts(typesByNamespace));
			}

			private static bool IsIgnored(Type type)
			{
				return type.FullName.StartsWith("System.Diagnostics.Contracts")
					|| type.Name.StartsWith("<")
					|| type.FullName.StartsWith("<")
					|| Check.That(type.Namespace).IsNullOrEmpty();
			}

			private IEnumerable<NamespacePartBinding> BindNamespaceParts(IEnumerable<Type> types)
			{
				return
					from type in types
					group type by type.Assembly into typesByAssembly
					orderby typesByAssembly.Key.FullName
					select new NamespacePartBinding(typesByAssembly.Key, BindTypes(types));
			}

			private IEnumerable<TypeBinding> BindTypes(IEnumerable<Type> types)
			{
				return
					from type in types
					let binding =  BindType(type)
					where binding != null
					orderby type.Name
					select binding;
			}

			private TypeBinding BindType(Type type)
			{
				if(type.IsEnum)
				{
					return _bindEnum(type);
				}
				else if(typeof(Notion).IsAssignableFrom(type))
				{
					return typeof(IAggregate).IsAssignableFrom(type) ? BindAggregate(type) : BindNotion(type);
				}
				else
				{
					return null;
				}
			}

			private NotionBinding BindAggregate(Type type)
			{
				return BindNotion(type, (traitBindings, fieldBindings) => new AggregateBinding(type, traitBindings, fieldBindings));
			}

			private NotionBinding BindNotion(Type type)
			{
				return BindNotion(type, (traitBindings, fieldBindings) => new NotionBinding(type, traitBindings, fieldBindings));
			}

			private NotionBinding BindNotion(Type type, Func<IEnumerable<TraitBinding>, IEnumerable<FieldBinding>, NotionBinding> bindingSelector)
			{
				var staticFields = GetStaticFields(type);

				var traitBindings = BindTraits(type, staticFields[StaticFieldType.Trait]);
				var fieldBindings = BindFields(type, staticFields[StaticFieldType.Field]);

				return bindingSelector(traitBindings, fieldBindings);
			}

			private enum StaticFieldType { Trait, Field, Ignored };

			private static ILookup<StaticFieldType, FieldInfo> GetStaticFields(Type type)
			{
				return type
					.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)
					.Where(staticField => !staticField.FieldType.ContainsGenericParameters)
					.ToLookup(staticField =>
					{
						if(typeof(Trait).IsAssignableFrom(staticField.FieldType))
						{
							return StaticFieldType.Trait;
						}
						else if(typeof(Field).IsAssignableFrom(staticField.FieldType))
						{
							return StaticFieldType.Field;
						}
						else
						{
							return StaticFieldType.Ignored;
						}
					});
			}

			private IEnumerable<TraitBinding> BindTraits(Type type, IEnumerable<FieldInfo> declarations)
			{
				return
					from declaration in declarations
					let declaredTrait = GetDeclared<Trait>(declaration, exception => _log.OnTraitDeclarationException(declaration, exception))
					where declaredTrait != null
					select new TraitBinding(declaredTrait, declaration);
			}

			private IEnumerable<FieldBinding> BindFields(Type type, IEnumerable<FieldInfo> declarations)
			{
				return
					from declaration in declarations
					let declaredField = GetDeclared<Field>(declaration, exception => _log.OnFieldDeclarationException(declaration, exception))
					where declaredField != null && declaredField.Trait == null
					join member in GetInstanceMembers(type) on declaredField.Name equals member.Name into fieldMembers
					from fieldMember in fieldMembers.DefaultIfEmpty()
					select new FieldBinding(declaredField, fieldMember ?? declaration);
			}

			private static T GetDeclared<T>(FieldInfo declaration, Action<Exception> logException)
			{
				try
				{
					var declaredField = (T) declaration.GetValue(null);

					if(declaredField == null)
					{
						throw new NullReferenceException();
					}

					return declaredField;
				}
				catch(Exception exception)
				{
					// TODO: Determine how to access declarations of fields of generic types (i.e. ConversionResult`1)

					logException(exception);

					return default(T);
				}
			}

			private static IEnumerable<MemberInfo> GetInstanceMembers(Type type)
			{
				var instanceMembersFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

				return type.GetProperties(instanceMembersFlags).Cast<MemberInfo>().Concat(type.GetFields(instanceMembersFlags));
			}
		}
	}
}