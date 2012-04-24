using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.String"/> starts with a value
	/// </summary>
	public sealed class CheckStartsWithAttribute : CheckAttribute
	{
		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="value">The value to look for at the start of the string</param>
		public CheckStartsWithAttribute(string value)
		{
			Contract.Requires(value != null);

			Value = value;
		}

		/// <summary>
		/// Initializes an attribute with the specified value
		/// </summary>
		/// <param name="value">The value to look for at the start of the string</param>
		/// <param name="comparisonType">The type of comparison to perform</param>
		public CheckStartsWithAttribute(string value, StringComparison comparisonType) : this(value)
		{
			ComparisonType = comparisonType;
		}

		/// <summary>
		/// Gets the value to look for at the start of the string
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// Gets the type of comparison to perform
		/// </summary>
		public StringComparison? ComparisonType { get; private set; }

		/// <summary>
		/// Gets an instance of <see cref="StartsWithMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="StartsWithMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return ComparisonType == null ? new StartsWithMethod(Value) : new StartsWithMethod(Value, ComparisonType.Value);
		}
	}
}