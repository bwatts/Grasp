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
		public static readonly Field<Rule> RuleField = Grasp.Field.On<NotRule>.For(x => x.Rule);

		internal NotRule(Rule rule) : base(RuleType.Not)
		{
			Rule = rule;
		}

		/// <summary>
		/// Gets the negated rule
		/// </summary>
		public Rule Rule { get { return GetValue(RuleField); } private set { SetValue(RuleField, value); } }
	}
}