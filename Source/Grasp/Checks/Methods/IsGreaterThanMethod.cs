using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.Reflection;

namespace Grasp.Checks.Methods
{
	/// <summary>
	/// Produces check rules for the <see cref="Checkable.IsGreaterThan{T}"/> method
	/// </summary>
	public sealed class IsGreaterThanMethod : ComparisonMethod
	{
		private readonly object _value;

		/// <summary>
		/// Initializes a method with the specified value
		/// </summary>
		/// <param name="value">The value to compare</param>
		public IsGreaterThanMethod(object value)
		{
			_value = value;
		}

		/// <summary>
		/// Gets the <see cref="Checkable.IsGreaterThan{T}"/> method
		/// </summary>
		/// <param name="targetType">The type of target data to which this check method is applied</param>
		/// <param name="checkType">The type of check</param>
		/// <returns>The <see cref="Checkable.IsGreaterThan{T}"/> method</returns>
		protected override MethodInfo GetMethod(Type targetType, Type checkType)
		{
			return typeof(Checkable).GetMethod("IsGreaterThan").MakeGenericMethod(targetType);
		}

		/// <summary>
		/// Gets the value to compare
		/// </summary>
		/// <param name="targetType">The type of target data to which this check method is applied</param>
		/// <returns>The value to compare</returns>
		protected override IEnumerable<object> GetCheckArguments(Type targetType)
		{
			yield return _value;
		}
	}
}