using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules.Visitor
{
	public class RewriteMemberRule : Behavior
	{
		Rule _newRule;
		MemberRule _memberRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_memberRule = Rule.Property(typeof(TestTarget).GetProperty("Target"), Rule.Constant(true));

			_newRule = Rule.Constant(false);

			_visitor = new TestRuleVisitor { NewRule = _newRule };
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_memberRule);
		}

		[Then]
		public void ResultHasOriginalType()
		{
			Assert.That(_resultRule.Type, Is.EqualTo(_memberRule.Type));
		}

		[Then]
		public void RuleIsNew()
		{
			var resultMemberRule = (MemberRule) _resultRule;

			Assert.That(resultMemberRule.Rule, Is.EqualTo(_newRule));
		}

		private class TestTarget
		{
			public int Target { get; set; }
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal Rule NewRule { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitConstant(ConstantRule node)
			{
				return NewRule;
			}
		}
	}
}