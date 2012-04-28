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
	public class VisitUnknownRuleType : Behavior
	{
		UnknownRule _unknownRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_unknownRule = new UnknownRule();

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_unknownRule);
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_unknownRule));
		}

		private class UnknownRule : Rule
		{
			public override RuleType Type
			{
				get { return (RuleType) (-1); }
			}
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}
		}
	}
}