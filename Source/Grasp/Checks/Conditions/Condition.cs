using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Grasp.Checks.Rules;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// A rule which applies to a particular target type and is uniquely identified by a key
	/// </summary>
	public class Condition : Notion
	{
		public static readonly Field<Rule> RuleField = Field.On<Condition>.For(x => x.Rule);
		public static readonly Field<ConditionKey> KeyField = Field.On<Condition>.For(x => x.Key);

		/// <summary>
		/// Initializes a condition with the specified rule and key
		/// </summary>
		/// <param name="rule">The rule which applies to the target type</param>
		/// <param name="key">The key which uniquely identifies this condition among those for all types</param>
		public Condition(Rule rule, ConditionKey key)
		{
			Contract.Requires(rule != null);
			Contract.Requires(key != null);

			Rule = rule;
			Key = key;
		}

		/// <summary>
		/// Initializes a condition with the specified rule and a key for specified target type
		/// </summary>
		/// <param name="rule">The rule which applies to the target type</param>
		/// <param name="targetType">The type to which the condition applies</param>
		public Condition(Rule rule, Type targetType) : this(rule, new ConditionKey(targetType))
		{}

		/// <summary>
		/// Initializes a condition with the specified rule and a key for specified target type and name
		/// </summary>
		/// <param name="rule">The rule which applies to the target type</param>
		/// <param name="targetType">The type to which the condition applies</param>
		/// <param name="name">The name which uniquely identifies the condition among all those for the target type</param>
		public Condition(Rule rule, Type targetType, string name) : this(rule, new ConditionKey(targetType, name))
		{}

		/// <summary>
		/// Gets the rule which applies to the target type
		/// </summary>
		public Rule Rule { get { return GetValue(RuleField); } private set { SetValue(RuleField, value); } }

		/// <summary>
		/// Gets the key which uniquely identifies this condition among those for all types
		/// </summary>
		public ConditionKey Key { get { return GetValue(KeyField); } private set { SetValue(KeyField, value); } }
	}

	/// <summary>
	/// A rule which applies to the specified target type and is uniquely identified by a key
	/// </summary>
	/// <typeparam name="TTarget">The type to which the condition applies</typeparam>
	public class Condition<TTarget> : Condition
	{
		/// <summary>
		/// Initializes a condition with the specified rule and a key for the target type
		/// </summary>
		/// <param name="rule">The rule which applies to the target type</param>
		public Condition(Rule rule) : base(rule, typeof(TTarget))
		{}

		/// <summary>
		/// Initializes a condition with the specified rule and a key for the target type and specified name
		/// </summary>
		/// <param name="rule">The rule which applies to the target type</param>
		/// <param name="name">The name which uniquely identifies the condition among all those for the target type</param>
		public Condition(Rule rule, string name) : base(rule, typeof(TTarget), name)
		{}
	}
}