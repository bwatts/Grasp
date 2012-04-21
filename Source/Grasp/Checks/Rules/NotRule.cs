using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents the negation of the result of a rule
	/// </summary>
	public sealed class NotRule : Rule
	{
		internal NotRule(Rule rule)
		{
			Rule = rule;
		}

		/// <summary>
		/// Gets <see cref="RuleType.Not"/>
		/// </summary>
		public override RuleType Type
		{
			get { return RuleType.Not; }
		}

		/// <summary>
		/// Gets the negated rule
		/// </summary>
		public Rule Rule { get; private set; }
	}
}