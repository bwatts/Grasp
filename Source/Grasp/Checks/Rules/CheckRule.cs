using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents the application of a check method to the target data
	/// </summary>
	public sealed class CheckRule : Rule
	{
		internal CheckRule(MethodInfo method, ReadOnlyCollection<object> checkArguments)
		{
			Method = method;
			CheckArguments = checkArguments;
		}

		/// <summary>
		/// Gets <see cref="RuleType.Check"/>
		/// </summary>
		public override RuleType Type
		{
			get { return RuleType.Check; }
		}

		/// <summary>
		/// Gets the check method
		/// </summary>
		public MethodInfo Method { get; private set; }

		/// <summary>
		/// Gets the arguments to the check method (without the base check)
		/// </summary>
		public ReadOnlyCollection<object> CheckArguments { get; private set; }
	}
}