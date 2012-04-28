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
	public class VisitCheckRule : Behavior
	{
		CheckRule _checkRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_checkRule = Rule.Check(Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsEven));

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_checkRule);
		}

		[Then]
		public void RoutesToVisitCheck()
		{
			Assert.That(_visitor.VisitedRule, Is.EqualTo(_checkRule));
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_checkRule));
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal CheckRule VisitedRule { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitCheck(CheckRule node)
			{
				VisitedRule = node;

				return base.VisitCheck(node);
			}
		}
	}
}