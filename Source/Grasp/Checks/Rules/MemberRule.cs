using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents the application of a rule to the value of a member of the target data
	/// </summary>
	public sealed class MemberRule : Rule
	{
		public static readonly Field<MemberInfo> MemberField = Grasp.Field.On<MemberRule>.For(x => x.Member);
		public static readonly Field<Type> MemberTypeField = Grasp.Field.On<MemberRule>.For(x => x.MemberType);
		public static readonly Field<Rule> RuleField = Grasp.Field.On<MemberRule>.For(x => x.Rule);

		internal MemberRule(RuleType type, MemberInfo member, Type memberType, Rule rule) : base(type)
		{
			Member = member;
			MemberType = memberType;
			Rule = rule;
		}

		/// <summary>
		/// Gets the member to which the rule is applied
		/// </summary>
		public MemberInfo Member { get { return GetValue(MemberField); } private set { SetValue(MemberField, value); } }

		/// <summary>
		/// Gets the type of the member's value
		/// </summary>
		public Type MemberType { get { return GetValue(MemberTypeField); } private set { SetValue(MemberTypeField, value); } }

		/// <summary>
		/// Gets the rule applied to the value of the member
		/// </summary>
		public Rule Rule { get { return GetValue(RuleField); } private set { SetValue(RuleField, value); } }
	}
}