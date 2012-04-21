using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents a constant value
	/// </summary>
	public sealed class ConstantRule : Rule
	{
		internal ConstantRule(bool passes)
		{
			Passes = passes;
		}

		/// <summary>
		/// Gets <see cref="RuleType.Constant"/>
		/// </summary>
		public override RuleType Type
		{
			get { return RuleType.Constant; }
		}

		/// <summary>
		/// Gets whether this rule passes
		/// </summary>
		public bool Passes { get; private set; }
	}
}