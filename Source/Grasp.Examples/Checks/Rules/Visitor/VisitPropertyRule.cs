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
	public class VisitPropertyRule : Behavior
	{
		MemberRule _propertyRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_propertyRule = Rule.Property(typeof(TestTarget).GetProperty("Target"), Rule.Constant(true));

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_propertyRule);
		}

		[Then]
		public void RoutesToVisitMember()
		{
			Assert.That(_visitor.VisitedRule, Is.EqualTo(_propertyRule));
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_propertyRule));
		}

		private class TestTarget
		{
			public int Target { get; set; }
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