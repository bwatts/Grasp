using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target value is equal to a specified value
	/// </summary>
	public sealed class CheckIsEqualToAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="value">The value to check for equality against the target value</param>
		public CheckIsEqualToAttribute(object value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets the value to check for equality against the target value
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// Gets an instance of <see cref="IsEqualToMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsEqualToMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsEqualToMethod(Value);
		}
	}
}