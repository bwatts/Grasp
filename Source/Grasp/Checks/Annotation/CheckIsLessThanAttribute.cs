using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is less than a specified value
	/// </summary>
	public sealed class CheckIsLessThanAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="value">The value to compare</param>
		public CheckIsLessThanAttribute(object value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value to compare
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// Gets an instance of <see cref="IsLessThanMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsLessThanMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsLessThanMethod(Value);
		}
	}
}