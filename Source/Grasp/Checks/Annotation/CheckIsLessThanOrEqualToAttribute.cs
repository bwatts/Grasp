using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is less than or equal to a specified value
	/// </summary>
	public sealed class CheckIsLessThanOrEqualToAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="value">The value to compare</param>
		public CheckIsLessThanOrEqualToAttribute(object value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value to compare
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// Gets an instance of <see cref="IsLessThanOrEqualToMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsLessThanOrEqualToMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsLessThanOrEqualToMethod(Value);
		}
	}
}