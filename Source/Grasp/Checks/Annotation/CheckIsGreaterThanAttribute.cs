using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is greater than a specified value
	/// </summary>
	public sealed class CheckIsGreaterThanAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="value">The value to compare</param>
		public CheckIsGreaterThanAttribute(object value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value to compare
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// Gets an instance of <see cref="IsGreaterThanMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsGreaterThanMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsGreaterThanMethod(Value);
		}
	}
}