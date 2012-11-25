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
	public sealed class ConditionRepository : Notion, IConditionRepository
	{
		public static readonly Field<ManyKeyed<ConditionKey, Condition>> _conditionsField = Field.On<ConditionRepository>.For(x => x._conditions);
		public static readonly Field<IConditionSource> _sourceField = Field.On<ConditionRepository>.For(x => x._source);
		public static readonly Field<bool> _conditionsLoadedField = Field.On<ConditionRepository>.For(x => x._conditionsLoaded);

		private ManyKeyed<ConditionKey, Condition> _conditions { get { return GetValue(_conditionsField); } set { SetValue(_conditionsField, value); } }
		private IConditionSource _source { get { return GetValue(_sourceField); } set { SetValue(_sourceField, value); } }
		private bool _conditionsLoaded { get { return GetValue(_conditionsLoadedField); } set { SetValue(_conditionsLoadedField, value); } }

		/// <summary>
		/// Initializes a repository with the specified condition source
		/// </summary>
		/// <param name="source">The source which provides effective conditions</param>
		public ConditionRepository(IConditionSource source)
		{
			Contract.Requires(source != null);

			_source = source;

			_conditions = new ManyKeyed<ConditionKey,Condition>();
		}

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
				_conditions.AsWriteable()[conditionGroup.Key] = MergeConditionGroup(conditionGroup.Key, conditionGroup.ToList());
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