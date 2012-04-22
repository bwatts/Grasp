using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Provides methods for merging sequences of rules
	/// </summary>
	public static class RuleSequenceMerge
	{
		/// <summary>
		/// Merges the specified sequence of rules into a single rule, eliminating redundancy where possible
		/// </summary>
		/// <param name="rules">The sequence of rules to merge</param>
		/// <returns>A single rule which has the semantics of the specified rules</returns>
		/// <exception cref="InvalidOperationException">The sequence has no rules</exception>
		public static Rule Merge(this IEnumerable<Rule> rules)
		{
			Contract.Requires(rules != null);

			return rules.Aggregate((left, right) => left.MergeWith(right));
		}
	}
}