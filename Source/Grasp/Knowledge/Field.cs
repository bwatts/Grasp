using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cloak;
using Cloak.Linq;
using Cloak.Reflection;

namespace Grasp.Knowledge
{
	/// <summary>
	/// A placeholder for a piece of data associated with a <see cref="Notion"/>
	/// </summary>
	public class Field
	{
		#region Factory
		/// <summary>
		/// Specify the class which has the instance member backed by the field
		/// </summary>
		/// <typeparam name="TOwner">The class which has the instance member backed by the field</typeparam>
		public static class On<TOwner> where TOwner : Notion
		{
			/// <summary>
			/// Specify the instance property backed by the field
			/// </summary>
			/// <typeparam name="TValue">The type of the member's value</typeparam>
			/// <param name="getProperty">A lambda expression which accesses the instance property backed by the field</param>
			/// <returns>A field which backs the specified instance member</returns>
			/// <exception cref="ArgumentException">
			/// Thrown if the expression does not access a property, -or- if the property is not declared on <typeparamref name="TOwner"/>
			/// </exception>
			public static Field<TValue> Backing<TValue>(Expression<Func<TOwner, TValue>> getProperty)
			{
				Contract.Requires(getProperty != null);

				var member = getProperty.GetMemberInfo();
				var property = member as PropertyInfo;

				if(property == null)
				{
					throw new ArgumentException(Resources.MemberIsNotProperty.FormatInvariant(member.Name), "getProperty");
				}

				if(property.DeclaringType != typeof(TOwner))
				{
					throw new ArgumentException(Resources.MemberNotDeclaredOnOwningType.FormatInvariant(property.Name, typeof(TOwner)), "getProperty");
				}

				return new Field<TValue>(typeof(TOwner), property.Name, false);
			}
		}

		/// <summary>
		/// Specify the class to which the field is attached
		/// </summary>
		/// <typeparam name="TTarget">The class to which the field is attached</typeparam>
		public static class AttachedTo<TTarget> where TTarget : Notion
		{
			/// <summary>
			/// Specify the class which declares and owns the attached field
			/// </summary>
			/// <typeparam name="TOwner">The class which declares and owns the attached field</typeparam>
			public static class By<TOwner>
			{
				/// <summary>
				/// Specify the static method which gets the value from an instance of <typeparamref name="TTarget"/>
				/// </summary>
				/// <typeparam name="TValue">The type of the attached field's value</typeparam>
				/// <param name="callGetter">A call to the static method which gets the value from an instance of <typeparamref name="TTarget"/></param>
				/// <returns>An attached field which contains the value accessed by the specified getter</returns>
				/// <exception cref="ArgumentException">
				/// Thrown if <paramref name="callGetter"/> does not represent a static method call, -or- the static method is not declared by <typeparamref name="TOwner"/>,
				/// -or- the static method name does not start with the "Get" prefix
				/// </exception>
				public static Field<TValue> For<TValue>(Expression<Func<TTarget, TValue>> callGetter)
				{
					Contract.Requires(callGetter != null);

					var getter = callGetter.GetMethodInfo();

					if(getter.DeclaringType != typeof(TOwner))
					{
						throw new ArgumentException(Resources.GetterMethodNotDeclaredOnOwningType.FormatInvariant(getter.Name, typeof(TOwner)), "callGetter");
					}

					if(!getter.IsStatic)
					{
						throw new ArgumentException(Resources.GetterMethodNotStatic.FormatInvariant(getter.Name, typeof(TOwner)), "callGetter");
					}

					if(!getter.Name.StartsWith(Resources.GetPrefix))
					{
						throw new ArgumentException(Resources.GetterMethodNameDoesNotStartWithGet.FormatInvariant(getter.Name), "callGetter");
					}

					var fieldName = getter.Name.Substring(Resources.GetPrefix.Length);

					return new Field<TValue>(typeof(TOwner), fieldName, true);
				}
			}
		}
		#endregion

		internal Field(Type ownerType, string name, Type valueType, bool isAttachable)
		{
			OwnerType = ownerType;
			Name = name;
			ValueType = valueType;
			IsAttachable = isAttachable;
		}

		/// <summary>
		/// Gets the type which declares this field
		/// </summary>
		public Type OwnerType { get; private set; }

		/// <summary>
		/// Gets the name of this field
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the type of value represented by this field
		/// </summary>
		public Type ValueType { get; private set; }

		/// <summary>
		/// Gets whether this field can be attached to objects other than the declaring type
		/// </summary>
		public bool IsAttachable { get; private set; }

		/// <summary>
		/// Gets whether null can be assigned to <see cref="ValueType"/>
		/// </summary>
		public bool IsNullable
		{
			get { return ValueType.IsAssignableNull(); }
		}

		/// <summary>
		/// Gets a textual representation of this field
		/// </summary>
		/// <returns>A textual representation of this field</returns>
		public override string ToString()
		{
			return Resources.Field.FormatInvariant(Name, ValueType);
		}
	}

	/// <summary>
	/// A placeholder for a piece of data associated with a <see cref="Notion"/>
	/// </summary>
	/// <typeparam name="TValue">The type of value represented by this field</typeparam>
	public sealed class Field<TValue> : Field
	{
		internal Field(Type ownerType, string name, bool isAttachable) : base(ownerType, name, typeof(TValue), isAttachable)
		{}
	}
}