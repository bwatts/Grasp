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
				/// <param name="getField">A call to the static method which accesses the declared field</param>
				/// <returns>An attached field which contains the value accessed by the specified getter</returns>
				/// <exception cref="ArgumentException">
				/// Thrown if <paramref name="getField"/> does not access a static field, -or- the static field is not declared by <typeparamref name="TOwner"/>,
				/// -or- the static field's name does not end with the "Field" suffix
				/// </exception>
				public static Field<TValue> Backing<TValue>(Expression<Func<Field<TValue>>> getField)
				{
					Contract.Requires(getField != null);

					var memberAccess = getField.Body as MemberExpression;
					var fieldGetter = memberAccess == null ? null : memberAccess.Member as FieldInfo;

					if(fieldGetter == null)
					{
						throw new ArgumentException(Resources.GetterDoesNotAccessField.FormatInvariant(getField));
					}

					if(!fieldGetter.IsStatic)
					{
						throw new ArgumentException(Resources.DeclaringFieldNotStatic.FormatInvariant(fieldGetter.Name, typeof(TOwner)), "getField");
					}

					if(fieldGetter.DeclaringType != typeof(TOwner))
					{
						throw new ArgumentException(Resources.DeclaringFieldNotDeclaredOnOwningType.FormatInvariant(fieldGetter.Name, typeof(TOwner)), "getField");
					}

					if(!fieldGetter.Name.EndsWith(Resources.FieldSuffix))
					{
						throw new ArgumentException(Resources.DeclaringFieldNameDoesNotEndWithSuffix.FormatInvariant(fieldGetter.Name, Resources.FieldSuffix), "getField");
					}

					var fieldName = fieldGetter.Name.Substring(0, fieldGetter.Name.Length - Resources.FieldSuffix.Length);

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
		/// Gets whether this field represents many values as opposed to single value
		/// </summary>
		public bool IsMany
		{
			get { return ValueType.IsGenericType && ValueType.GetGenericTypeDefinition() == typeof(Many<>); }
		}

		/// <summary>
		/// Gets the type of element in this field, if <see cref="IsMany"/> is true
		/// </summary>
		/// <returns>The type of element in this field</returns>
		public Type GetManyElementType()
		{
			Contract.Requires(IsMany);

			return ValueType.GetGenericArguments().Single();
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

		public TValue Get(Notion notion)
		{
			return ((IFieldContext) notion).GetValue(this);
		}

		public void Set(Notion notion, TValue value)
		{
			((IFieldContext) notion).SetValue(this, value);
		}
	}
}