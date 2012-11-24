using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsEqualTo{T}(ICheckable{T}, T)"/> method
	/// </summary>
	public sealed class IsEqualToMethod : CheckMethod
	{
		public static readonly Field<object> _valueField = Field.On<IsEqualToMethod>.For(x => x._value);

		private object _value { get { return GetValue(_valueField); } set { SetValue(_valueField, value); } }

		/// <summary>
		/// Initializes a method with the specified value
		/// </summary>
		/// <param name="value">The value to check for equality against the target value</param>
		public IsEqualToMethod(object value)
		{
			_value = value;
		}

		/// <summary>
		/// Indicates that all target types are supported
		/// </summary>
		/// <param name="targetType">The target type to check</param>
		/// <returns>True</returns>
		protected override bool SupportsTargetType(Type targetType)
		{
			return true;
		}

		/// <summary>
		/// Gets a closed version of the <see cref="Checkable.IsEqualTo{T}(ICheckable{T}, T)"/> method using the specified target type
		/// </summary>
		/// <param name="targetType">The target type to check</param>
		/// <param name="checkType">The type of check</param>
		/// <returns>A closed version of the <see cref="Checkable.IsEqualTo{T}(ICheckable{T}, T)"/> method using the specified target type</returns>
		protected override MethodInfo GetMethod(Type targetType, Type checkType)
		{
			return typeof(Checkable)
				.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.Where(method => method.Name == "IsEqualTo")
				.Where(method => method.GetParameters().Length == 2)
				.First()
				.MakeGenericMethod(targetType);
		}

		/// <summary>
		/// Gets the value to check for equality against the target value
		/// </summary>
		/// <param name="targetType">The target type to check</param>
		/// <returns>Gets the value to check for equality against the target value</returns>
		protected override IEnumerable<object> GetCheckArguments(Type targetType)
		{
			yield return _value;
		}
	}
}