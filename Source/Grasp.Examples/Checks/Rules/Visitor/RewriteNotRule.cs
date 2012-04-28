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
	public class RewriteNotRule : Behavior
	{
		Rule _newRule;
		NotRule _notRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_notRule = Rule.Not(Rule.Constant(true));

			_newRule = Rule.Constant(false);

			_visitor = new TestRuleVisitor { NewRule = _newRule };
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_notRule);
		}

		[Then]
		public void ResultHasNotType()
		{
			Assert.That(_resultRule.Type, Is.EqualTo(RuleType.Not));
		}

		[Then]
		public void RuleIsNew()
		{
			var resultNotRule = (NotRule) _resultRule;

			Assert.That(resultNotRule.Rule, Is.EqualTo(_newRule));
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