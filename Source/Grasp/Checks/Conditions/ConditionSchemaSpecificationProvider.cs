using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Conditions
{
	internal sealed class ConditionSchemaSpecificationProvider : ISpecificationProvider
	{
		private readonly IDictionary<Rule, object> _ruleFunctions = new Dictionary<Rule, object>();

		#region ISpecificationProvider

		public Specification<T> CreateSpecification<T>(T target, Rule rule)
		{
			return new Specification<T>(target, this, rule);
		}

		public bool Apply<T>(T target, Rule rule)
		{
			// Each rule is associated with a unique condition in a schema. This effectively lets us cache the compiled version of each condition.

			Func<T, bool> ruleFunction;

			lock(_ruleFunctions)
			{
				ruleFunction = GetCachedRuleFunction<T>(rule);

				if(ruleFunction == null)
				{
					ruleFunction = rule.ToFunction<T>();

					_ruleFunctions[rule] = ruleFunction;
				}
			}

			return ruleFunction(target);
		}
		#endregion

		private Func<T, bool> GetCachedRuleFunction<T>(Rule rule)
		{
			object ruleFunction;

			_ruleFunctions.TryGetValue(rule, out ruleFunction);

			return (Func<T, bool>) ruleFunction;
		}
	}
}