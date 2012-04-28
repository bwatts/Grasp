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
	public class VisitFieldRule : Behavior
	{
		MemberRule _fieldRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_fieldRule = Rule.Field(typeof(TestTarget).GetField("Target"), Rule.Constant(true));

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_fieldRule);
		}

		[Then]
		public void RoutesToVisitMember()
		{
			Assert.That(_visitor.VisitedRule, Is.EqualTo(_fieldRule));
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_fieldRule));
		}

		private class TestTarget
		{
			public int Target = 0;
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal MemberRule VisitedRule { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitMember(MemberRule node)
			{
				VisitedRule = node;

				return base.VisitMember(node);
			}
		}
	}
}