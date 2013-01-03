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

namespace Grasp.Semantics.Discovery.Reflection
{
	public static class AssemblyBinder
	{
		public static ReflectionBinding BindDomainModel(this Assembly assembly, FullName name)
		{
			Contract.Requires(name != null);
			Contract.Requires(assembly != null);

			return new[] { assembly }.BindDomainModel(name);
		}

		public static ReflectionBinding BindDomainModel(this IEnumerable<Assembly> assemblies, FullName name)
		{
			Contract.Requires(name != null);
			Contract.Requires(assemblies != null);

			return new ReflectionBinding(name, GetNamespaceBindings(assemblies));
		}

		private static IEnumerable<NamespaceBinding> GetNamespaceBindings(IEnumerable<Assembly> assemblies)
		{
			assemblies = assemblies.Concat(new[] { Assembly.GetExecutingAssembly() }).Distinct();

			return new AssemblyBinderContext().GetNamespaceBindings(assemblies);
		}

		private sealed class AssemblyBinderContext
		{
			private readonly Func<Type, EnumBinding> _getEnumBinding;

			internal AssemblyBinderContext()
			{
				_getEnumBinding = type => new EnumBinding(type);

				_getEnumBinding = _getEnumBinding.Cached();
			}

			internal IEnumerable<NamespaceBinding> GetNamespaceBindings(IEnumerable<Assembly> assemblies)
			{
				return
					from assembly in assemblies
					from type in assembly.GetTypes()
					where !IgnoreType(type)
					group type by type.Namespace into typesByNamespace
					orderby typesByNamespace.Key
					select new NamespaceBinding(typesByNamespace.Key, GetNamespacePartBindings(typesByNamespace));
			}

			private static bool IgnoreType(Type type)
			{
				return type.FullName.StartsWith("System.Diagnostics.Contracts")
					|| type.Name.StartsWith("<")
					|| type.FullName.StartsWith("<")
					|| Check.That(type.Namespace).IsNullOrEmpty();
			}

			private IEnumerable<NamespacePartBinding> GetNamespacePartBindings(IEnumerable<Type> types)
			{
				return
					from type in types
					group type by type.Assembly into typesByAssembly
					orderby typesByAssembly.Key.FullName
					select new NamespacePartBinding(typesByAssembly.Key, GetTypeBindings(types));
			}

			private IEnumerable<TypeBinding> GetTypeBindings(IEnumerable<Type> types)
			{
				return
					from type in types
					orderby type.Name
					let binding = type.IsEnum ? _getEnumBinding(type) : GetTypeBinding(type)
					where binding != null
					select binding;
			}

			private static TypeBinding GetTypeBinding(Type type)
			{
				return typeof(Notion).IsAssignableFrom(type) ? GetNotionBinding(type) : GetFieldAttacherBinding(type) as TypeBinding;
			}

			private static NotionBinding GetNotionBinding(Type type)
			{
				return new NotionBinding(type, GetFieldBindings(type));
			}

			private static FieldAttacherBinding GetFieldAttacherBinding(Type type)
			{
				var attachedFieldBindings = GetFieldBindings(type).ToMany();

				var nonAttachedField = attachedFieldBindings.FirstOrDefault(binding => !binding.Field.IsAttached);

				if(nonAttachedField != null)
				{
					throw new GraspException(Resources.NonAttachedFieldOnNonNotionType.FormatInvariant(nonAttachedField.Field.FullName, type));
				}

				return !attachedFieldBindings.Any() ? null : new FieldAttacherBinding(type, attachedFieldBindings);
			}

			private static IEnumerable<FieldBinding> GetFieldBindings(Type type)
			{
				var members = type
					.GetProperties(BindingFlags.Public | BindingFlags.Instance)
					.Cast<MemberInfo>()
					.Concat(type.GetFields(BindingFlags.Public | BindingFlags.Instance));

				var fields =
					from staticField in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
					where !staticField.FieldType.ContainsGenericParameters
					where typeof(Field).IsAssignableFrom(staticField.FieldType)
					let declaredField = GetDeclaredField(staticField)
					where declaredField != null
					select new { Static = staticField, Declared = declaredField };

				return
					from field in fields
					join member in members on field.Declared.Name equals member.Name into fieldMembers
					from member in fieldMembers.DefaultIfEmpty()
					select new FieldBinding(field.Declared, member ?? field.Static);
			}

			private static Field GetDeclaredField(FieldInfo staticField)
			{
				try
				{
					return (Field) staticField.GetValue(null);
				}
				catch
				{
					return null;
				}
			}
		}
	}
}