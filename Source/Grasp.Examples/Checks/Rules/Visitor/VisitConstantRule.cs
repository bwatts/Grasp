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
	public class VisitConstantRule : Behavior
	{
		ConstantRule _constantRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_constantRule = Rule.Constant(true);

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_constantRule);
		}

		[Then]
		public void RoutesToVisitConstant()
		{
			Assert.That(_visitor.VisitedRule, Is.EqualTo(_constantRule));
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_constantRule));
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal ConstantRule VisitedRule { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitConstant(ConstantRule node)
			{
				VisitedRule = node;

				return base.VisitConstant(node);
			}
		}
	}
}