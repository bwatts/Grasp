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
	public class VisitNotRule : Behavior
	{
		NotRule _notRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_notRule = Rule.Not(Rule.Constant(true));

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_notRule);
		}

		[Then]
		public void RoutesToVisitNot()
		{
			Assert.That(_visitor.VisitedRule, Is.EqualTo(_notRule));
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_notRule));
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal NotRule VisitedRule { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitNot(NotRule node)
			{
				VisitedRule = node;

				return base.VisitNot(node);
			}
		}
	}
}