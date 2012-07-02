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

			ReflectionBinding.NamespaceBindingsField.Set(x, new Many<NamespaceBinding>(GetNamespaceBindings(assemblies)));

			return x;
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
				_getEnumBinding = CreateEnumBinding;

				_getEnumBinding = _getEnumBinding.Cached();
			}

			internal IEnumerable<NamespaceBinding> GetNamespaceBindings(IEnumerable<Assembly> assemblies)
			{
				var namespaces =
					from assembly in assemblies
					from type in assembly.GetTypes()
					where type.FullName != "System.Diagnostics.Contracts.RuntimeContractsFlags"
					where type.IsEnum || typeof(Notion).IsAssignableFrom(type) || type == typeof(Field)
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

					NamespaceBinding.NameField.Set(x, @namespace.Name);
					NamespaceBinding.PartBindingsField.Set(x, @namespace.PartBindings);

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

					NamespacePartBinding.AssemblyField.Set(x, part.Assembly);
					NamespacePartBinding.TypeBindingsField.Set(x, part.TypeBindings);

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

				EnumBinding.TypeField.Set(x, type);

				return x;
			}

			private static EntityBinding GetObjectBinding(Type type)
			{
				var x = new EntityBinding();

				TypeBinding.TypeField.Set(x, type);
				EntityBinding.MemberBindingsField.Set(x, new Many<MemberBinding>(GetMemberBindings(type)));

				return x;
			}

			private static IEnumerable<MemberBinding> GetMemberBindings(Type type)
			{
				foreach(var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(property => property.Name))
				{
					var x = new PropertyInfoBinding();

					var field = GetField(property);

					// TODO: Remove base info field binding after implementing shadowing
					MemberBinding.InfoField.Set(x, property);
					MemberBinding.FieldField.Set(x, field);
					PropertyInfoBinding.InfoField.Set(x, property);

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
					MemberBinding.InfoField.Set(x, field);
					MemberBinding.FieldField.Set(x, GetField(field));
					FieldInfoBinding.InfoField.Set(x, field);

					// TODO: Don't allow unmatched fields
					if(x.Field != null)
					{
						yield return x;
					}
				}
			}

			private static Field GetField(MemberInfo member)
			{


				// TODO: Better fix than this hackety hack
				var matchingFields =
					from staticField in member.DeclaringType.GetFields(BindingFlags.Public | BindingFlags.Static)
					where !staticField.FieldType.ContainsGenericParameters
					where typeof(Field).IsAssignableFrom(staticField.FieldType)
					let field = GetField(staticField)
					where field != null && field.Name == member.Name
					select field;




				// TODO: Optimize this to do field lookups once

				//var matchingFields =
				//  from staticField in member.DeclaringType.GetFields(BindingFlags.Public | BindingFlags.Static)
				//  where !staticField.FieldType.ContainsGenericParameters
				//  where typeof(Field).IsAssignableFrom(staticField.FieldType)
				//  let field = (Field) staticField.GetValue(null)
				//  where field.Name == member.Name
				//  select field;

				// TODO: Don't allow unmatched fields
				//return matchingFields.Single();

				return matchingFields.SingleOrDefault();
			}






			private static Field GetField(FieldInfo staticField)
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