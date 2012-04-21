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
		private readonly RuleType _type;

		internal MemberRule(RuleType type, MemberInfo member, Type memberType, Rule rule)
		{
			_type = type;
			Member = member;
			MemberType = memberType;
			Rule = rule;
		}

		/// <summary>
		/// Gets the type of this member rule
		/// </summary>
		public override RuleType Type
		{
			get { return _type; }
		}

		/// <summary>
		/// Gets the member to which the rule is applied
		/// </summary>
		public MemberInfo Member { get; private set; }

		/// <summary>
		/// Gets the type of the member's value
		/// </summary>
		public Type MemberType { get; private set; }

		/// <summary>
		/// Gets the rule applied to the value of the member
		/// </summary>
		public Rule Rule { get; private set; }
	}
}