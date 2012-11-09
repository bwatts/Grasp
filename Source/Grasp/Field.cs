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

namespace Grasp
{
	/// <summary>
	/// A placeholder for a piece of data associated with a <see cref="Notion"/>
	/// </summary>
	public class Field
	{
		#region Factory
		/// <summary>
		/// Specify the class which declares the field
		/// </summary>
		/// <typeparam name="TOwner">The class which declares the field</typeparam>
		public static class On<TOwner> where TOwner : Notion
		{
			/// <summary>
			/// Specify the instance property: x => x.TheProperty
			/// </summary>
			/// <typeparam name="TValue">The type of the property's value</typeparam>
			/// <param name="getProperty">A lambda expression which accesses the instance property backed by the field</param>
			/// <returns>A field which backs the specified instance member</returns>
			/// <exception cref="ArgumentException">
			/// Thrown if the expression does not access a property, -or- if the property is not declared by <typeparamref name="TOwner"/>
			/// </exception>
			public static Field<TValue> For<TValue>(Expression<Func<TOwner, TValue>> getProperty)
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
			/// Specify the static class which declares and owns the attached field
			/// </summary>
			public static class By
			{
				/// <summary>
				/// Specify the static class which declares and owns the attached field (static classes cannot be used as type parameters)
				/// </summary>
				/// <param name="ownerType">The static class which declares and owns the attached field</param>
				/// <returns>A builder to configure the backing field</returns>
				public static Builder Static(Type ownerType)
				{
					return new Builder(ownerType);
				}

				/// <summary>
				/// Configures the backing field of an attached field
				/// </summary>
				public sealed class Builder
				{
					internal Builder(Type ownerType)
					{
						OwnerType = ownerType;
					}

					/// <summary>
					/// Gets the class which declares and owns the attached field
					/// </summary>
					public Type OwnerType { get; private set; }

					/// <summary>
					/// Specify the attached field being defined: () => TheField
					/// </summary>
					/// <typeparam name="TValue">The type of the attached field's value</typeparam>
					/// <param name="getField">A call which accesses the declared field</param>
					/// <returns>An attached field defined by the declared field</returns>
					/// <exception cref="ArgumentException">
					/// Thrown if <paramref name="getField"/> does not access a static field, -or- the static field is not declared by <see cref="OwnerType"/>,
					/// -or- the static field's name does not end with the "Field" suffix
					/// </exception>
					public Field<TValue> For<TValue>(Expression<Func<Field<TValue>>> getField)
					{
						Contract.Requires(getField != null);

						var memberAccess = getField.Body as MemberExpression;

						var field = memberAccess == null ? null : memberAccess.Member as FieldInfo;

						if(field == null)
						{
							throw new ArgumentException(Resources.GetterDoesNotAccessField.FormatInvariant(getField));
						}

						if(!field.IsStatic)
						{
							throw new ArgumentException(Resources.DeclaringFieldNotStatic.FormatInvariant(field.Name, OwnerType), "getField");
						}

						if(field.DeclaringType != OwnerType)
						{
							throw new ArgumentException(Resources.DeclaringFieldNotDeclaredOnOwningType.FormatInvariant(field.Name, OwnerType), "getField");
						}

						if(!field.Name.EndsWith(Resources.FieldSuffix))
						{
							throw new ArgumentException(Resources.DeclaringFieldNameDoesNotEndWithSuffix.FormatInvariant(field.Name, Resources.FieldSuffix), "getField");
						}

						var fieldName = field.Name.Substring(0, field.Name.Length - Resources.FieldSuffix.Length);

						return new Field<TValue>(OwnerType, fieldName, true);
					}
				}
			}

			/// <summary>
			/// Specify the class which declares and owns the attached field
			/// </summary>
			/// <typeparam name="TOwner">The class which declares and owns the attached field</typeparam>
			public static class By<TOwner>
			{
				/// <summary>
				/// Specify the attached field being defined: () => TheField
				/// </summary>
				/// <typeparam name="TValue">The type of the attached field's value</typeparam>
				/// <param name="getField">A lambda expression which accesses the attached field</param>
				/// <returns>An attached field defined by the declared field</returns>
				/// <exception cref="ArgumentException">
				/// Thrown if <paramref name="getField"/> does not access a static field, -or- the static field is not declared by <see cref="OwnerType"/>,
				/// -or- the static field's name does not end with the "Field" suffix
				/// </exception>
				public static Field<TValue> For<TValue>(Expression<Func<Field<TValue>>> getField)
				{
					return By.Static(typeof(TOwner)).For(getField);
				}
			}
		}
		#endregion

		public static readonly object UnsetValue = new object();

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

		public object Get(Notion notion)
		{
			return ((IFieldContext) notion).GetValue(this);
		}

		public bool TryGet(Notion notion, out object value)
		{
			return ((IFieldContext) notion).TryGetValue(this, out value);
		}

		public void Set(Notion notion, object value)
		{
			((IFieldContext) notion).SetValue(this, value);
		}

		public bool IsUnset(Notion notion)
		{
			return ((IFieldContext) notion).GetValue(this) == UnsetValue;
		}

		public bool IsSet(Notion notion)
		{
			return !IsUnset(notion);
		}

		public FieldBinding Bind(object value)
		{
			// TODO: Type/assignability checking?

			return new FieldBinding(this, value);
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

		public new TValue Get(Notion notion)
		{
			return ((IFieldContext) notion).GetValue(this);
		}

		public bool TryGet(Notion notion, out TValue value)
		{
			return ((IFieldContext) notion).TryGetValue(this, out value);
		}

		public void Set(Notion notion, TValue value)
		{
			((IFieldContext) notion).SetValue(this, value);
		}

		public FieldBinding<TValue> Bind(TValue value)
		{
			// TODO: Type/assignability checking?

			return new FieldBinding<TValue>(this, value);
		}
	}
}