using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// A set of conditions indexed and cached by key
	/// </summary>
	public sealed class ConditionRepository : IConditionRepository
	{
		private readonly IDictionary<ConditionKey, Condition> _conditions = new Dictionary<ConditionKey, Condition>();
		private readonly IConditionSource _source;
		private bool _conditionsLoaded;

		/// <summary>
		/// Initializes a repository with the specified condition source
		/// </summary>
		/// <param name="source">The source which provides effective conditions</param>
		public ConditionRepository(IConditionSource source)
		{
			Contract.Requires(source != null);

			_source = source;
		}

		#region IConditionRepository
		/// <summary>
		/// Gets the condition with the specified key, or null if there is no condition with a matching key
		/// </summary>
		/// <param name="key">The key of the condition to retrieve</param>
		/// <returns>The condition with the specified key, or null if there is no condition with a matching key</returns>
		public Condition GetCondition(ConditionKey key)
		{
			EnsureConditionsLoaded();

			Condition condition;

			_conditions.TryGetValue(key, out condition);

			return condition;
		}
		#endregion

		private void EnsureConditionsLoaded()
		{
			if(!_conditionsLoaded)
			{
				lock(_conditions)
				{
					if(!_conditionsLoaded)
					{
						LoadConditions();

						_conditionsLoaded = true;
					}
				}
			}
		}

		private void LoadConditions()
		{
			foreach(var conditionGroup in _source.GetConditions().GroupBy(condition => condition.Key))
			{
				_conditions[conditionGroup.Key] = MergeConditionGroup(conditionGroup.Key, conditionGroup.ToList());
			}
		}

		private static Condition MergeConditionGroup(ConditionKey key, IList<Condition> conditions)
		{
			return conditions.Count == 1 ? conditions.Single() : new Condition(MergeRules(conditions), key);
		}

		private static Rule MergeRules(IList<Condition> conditions)
		{
			return conditions.Select(condition => condition.Rule).Merge();
		}
	}
}