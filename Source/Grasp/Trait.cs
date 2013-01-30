using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Linq;

namespace Grasp
{
	/// <summary>
	/// A set of attached state and associated behavior
	/// </summary>
	public class Trait
	{
		/// <summary>
		/// Declares a trait for the specified type, targeting <see cref="Notion"/>
		/// </summary>
		/// <param name="declaringType">The type which declares the trait</param>
		/// <returns>A trait for the specified type, targeting <see cref="Notion"/></returns>
		public static Trait DeclaredBy(Type declaringType)
		{
			Contract.Requires(declaringType != null);

			return new Trait(declaringType, typeof(Notion));
		}

		/// <summary>
		/// Declares a trait for the specified type, targeting <see cref="Notion"/>
		/// </summary>
		/// <typeparam name="T">The type which declares the trait</typeparam>
		/// <returns>A trait for the specified type, targeting <see cref="Notion"/></returns>
		public static Trait DeclaredBy<T>()
		{
			return new Trait(typeof(T), typeof(Notion));
		}

		private readonly ConcurrentDictionary<string, Field> _fieldsByFullName = new ConcurrentDictionary<string, Field>();

		internal Trait(Type declaringType, Type targetType)
		{
			DeclaringType = declaringType;
			TargetType = targetType;
		}

		/// <summary>
		/// Gets the type which declares this trait
		/// </summary>
		public Type DeclaringType { get; private set; }

		/// <summary>
		/// Gets the type targeted by this trait
		/// </summary>
		public Type TargetType { get; private set; }

		/// <summary>
		/// Gets the fields associated with this trait
		/// </summary>
		public IEnumerable<Field> Fields
		{
			get { return _fieldsByFullName.Values.Select(field => field); }
		}

		/// <summary>
		/// Declares an attached field associated with this trait
		/// </summary>
		/// <param name="getter">The function which accesses the member that declares the field</param>
		/// <returns>An attached field owned by this trait's declaring type</returns>
		public Field<TValue> Field<TValue>(Expression<Func<Field<TValue>>> getter)
		{
			Contract.Requires(getter != null);

			var field = CreateField(getter);

			if(!_fieldsByFullName.TryAdd(field.FullName, field))
			{
				throw new ArgumentException(Resources.DuplicateTraitField.FormatInvariant(field.FullName));
			}

			return field;
		}

		private Field<TValue> CreateField<TValue>(Expression<Func<Field<TValue>>> getter)
		{
			var fieldName = ValidateDeclarationAndGetName(getter);

			return new Field<TValue>(TargetType, DeclaringType, fieldName, this);
		}

		private string ValidateDeclarationAndGetName(LambdaExpression getter)
		{
			var field = getter.GetFieldInfo();

			if(field.DeclaringType != DeclaringType)
			{
				throw new ArgumentException(Resources.DeclaredFieldNotOnTraitDeclaringType.FormatInvariant(field.Name, DeclaringType), "getter");
			}

			if(!field.IsStatic)
			{
				throw new ArgumentException(Resources.DeclaredFieldNotStatic.FormatInvariant(DeclaringType, field.Name), "getter");
			}

			if(!field.Name.EndsWith(Resources.FieldSuffix))
			{
				throw new ArgumentException(Resources.DeclaredFieldNameDoesNotHaveSuffix.FormatInvariant(DeclaringType, field.Name, Resources.FieldSuffix), "getter");
			}

			return field.Name.Substring(0, field.Name.Length - Resources.FieldSuffix.Length);
		}
	}

	public sealed class Trait<TTarget> : Trait
	{
		/// <summary>
		/// Declares a trait for the specified type, targeting <see cref="TTarget"/>
		/// </summary>
		/// <param name="declaringType">The type which declares the trait</param>
		/// <returns>A trait for the specified type, targeting <see cref="TTarget"/></returns>
		public static new Trait<TTarget> DeclaredBy(Type declaringType)
		{
			Contract.Requires(declaringType != null);

			return new Trait<TTarget>(declaringType);
		}

		/// <summary>
		/// Declares a trait for the specified type, targeting <see cref="TTarget"/>
		/// </summary>
		/// <typeparam name="T">The type which declares the trait</typeparam>
		/// <returns>A trait for the specified type, targeting <see cref="TTarget"/></returns>
		public static new Trait<TTarget> DeclaredBy<T>()
		{
			return new Trait<TTarget>(typeof(T));
		}

		internal Trait(Type declaringType) : base(declaringType, typeof(TTarget))
		{}
	}
}