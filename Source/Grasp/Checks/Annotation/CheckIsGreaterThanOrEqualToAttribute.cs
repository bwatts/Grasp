using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is greater than or equal to a specified value
	/// </summary>
	public sealed class CheckIsGreaterThanOrEqualToAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="value">The value to compare</param>
		public CheckIsGreaterThanOrEqualToAttribute(object value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value to compare
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// Gets an instance of <see cref="IsGreaterThanOrEqualToMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsGreaterThanOrEqualToMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsGreaterThanOrEqualToMethod(Value);
		}
	}
}