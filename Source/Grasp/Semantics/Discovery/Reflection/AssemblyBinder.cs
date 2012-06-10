using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak;
using Grasp.Knowledge;
using Grasp.Semantics.Relationships;

namespace Grasp.Semantics.Discovery.Reflection
{
	public static class AssemblyBinder
	{
		public static ReflectionBinding Bind(this Assembly assembly)
		{
			Contract.Requires(assembly != null);

			return Bind(new[] { assembly });
		}

		public static ReflectionBinding Bind(this IEnumerable<Assembly> assemblies)
		{
			Contract.Requires(assemblies != null);

			var x = new ReflectionBinding();

			x.SetValue(ReflectionBinding.NamespaceBindingsField, new Many<NamespaceBinding>(GetNamespaceBindings(assemblies)));

			return x;
		}

		private static IEnumerable<NamespaceBinding> GetNamespaceBindings(IEnumerable<Assembly> assemblies)
		{
			return new AssemblyBinderContext().GetNamespaceBindings(assemblies);
		}

		private sealed class AssemblyBinderContext
		{
			private readonly Func<Type, EnumBinding> _getEnumBinding;

			internal AssemblyBinderContext()
			{
				_getEnumBinding = CreateEnumBinding;

				_getEnumBinding = _getEnumBinding.Cached();
			}

			internal IEnumerable<NamespaceBinding> GetNamespaceBindings(IEnumerable<Assembly> assemblies)
			{
				var namespaces =
					from assembly in assemblies
					from type in assembly.GetTypes()
					where type.FullName != "System.Diagnostics.Contracts.RuntimeContractsFlags"
					where type.IsEnum || typeof(Notion).IsAssignableFrom(type)
					group type by type.Namespace into typesByNamespace
					orderby typesByNamespace.Key
					select new
					{
						Name = typesByNamespace.Key,
						PartBindings = new Many<NamespacePartBinding>(GetNamespacePartBindings(typesByNamespace))
					};

				foreach(var @namespace in namespaces)
				{
					var x = new NamespaceBinding();

					x.SetValue(NamespaceBinding.NameField, @namespace.Name);
					x.SetValue(NamespaceBinding.PartBindingsField, @namespace.PartBindings);

					yield return x;
				}
			}

			private IEnumerable<NamespacePartBinding> GetNamespacePartBindings(IEnumerable<Type> types)
			{
				var parts =
					from type in types
					group type by type.Assembly into typesByAssembly
					orderby typesByAssembly.Key.FullName
					select new
					{
						Assembly = typesByAssembly.Key,
						TypeBindings = new Many<TypeBinding>(GetTypeBindings(typesByAssembly))
					};

				foreach(var part in parts)
				{
					var x = new NamespacePartBinding();

					x.SetValue(NamespacePartBinding.AssemblyField, part.Assembly);
					x.SetValue(NamespacePartBinding.TypeBindingsField, part.TypeBindings);

					yield return x;
				}
			}

			private IEnumerable<TypeBinding> GetTypeBindings(IEnumerable<Type> types)
			{
				return
					from type in types
					orderby type.Name
					select type.IsEnum ? _getEnumBinding(type) : GetObjectBinding(type) as TypeBinding;
			}

			private static EnumBinding CreateEnumBinding(Type type)
			{
				var x = new EnumBinding();

				x.SetValue(EnumBinding.TypeField, type);

				return x;
			}

			private static ObjectBinding GetObjectBinding(Type type)
			{
				var x = new ObjectBinding();

				x.SetValue(TypeBinding.TypeField, type);
				x.SetValue(ObjectBinding.MemberBindingsField, new Many<MemberBinding>(GetMemberBindings(type)));

				return x;
			}

			private static IEnumerable<MemberBinding> GetMemberBindings(Type type)
			{
				foreach(var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(property => property.Name))
				{
					var x = new PropertyInfoBinding();

					var field = GetField(property);

					// TODO: Remove base info field binding after implementing shadowing
					x.SetValue(MemberBinding.InfoField, property);
					x.SetValue(MemberBinding.FieldField, field);
					x.SetValue(PropertyInfoBinding.InfoField, property);

					// TODO: Don't allow unmatched fields
					if(field != null)
					{
						yield return x;
					}
				}

				foreach(var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance).OrderBy(field => field.Name))
				{
					var x = new FieldInfoBinding();

					// TODO: Remove base info field binding after implementing shadowing
					x.SetValue(MemberBinding.InfoField, field);
					x.SetValue(MemberBinding.FieldField, GetField(field));
					x.SetValue(FieldInfoBinding.InfoField, field);

					// TODO: Don't allow unmatched fields
					if(x.Field != null)
					{
						yield return x;
					}
				}
			}

			private static Field GetField(MemberInfo member)
			{
				// TODO: Optimize this to do field lookups once

				var matchingFields =
					from staticField in member.DeclaringType.GetFields(BindingFlags.Public | BindingFlags.Static)
					where !staticField.FieldType.ContainsGenericParameters
					where typeof(Field).IsAssignableFrom(staticField.FieldType)
					where !staticField.FieldType.IsGenericType	// TODO: Remove workaround
					let field = (Field) staticField.GetValue(null)
					where field.Name == member.Name
					select field;

				// TODO: Don't allow unmatched fields
				//return matchingFields.Single();

				return matchingFields.SingleOrDefault();
			}
		}
	}
}