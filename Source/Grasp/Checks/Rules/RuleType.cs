using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// The types of rules in a rule tree
	/// </summary>
	public enum RuleType
	{
		/// <summary>
		/// A rule which applies an individual check to the target data
		/// </summary>
		Check,

		/// <summary>
		/// A rule representing a result rather than a definition
		/// </summary>
		Result,

		/// <summary>
		/// A rule which applies a lambda expression to the target data
		/// </summary>
		Lambda,

		/// <summary>
		/// A rule which performs a logical AND on the results of other rules
		/// </summary>
		And,

		/// <summary>
		/// A rule which performs a logical OR on the results of other rules
		/// </summary>
		Or,

		/// <summary>
		/// A rule which performs a logical XOR on the results of other rules
		/// </summary>
		ExclusiveOr,

		/// <summary>
		/// A rule which performs a logical NOT on the result of another rule
		/// </summary>
		Not,

		/// <summary>
		/// A rule which applies a rule to a property of the target data
		/// </summary>
		Property,

		/// <summary>
		/// A rule which applies a rule to a field of the target data
		/// </summary>
		Field,

		/// <summary>
		/// A rule which applies a rule to the return value of a method of the target data
		/// </summary>
		Method
	}
}