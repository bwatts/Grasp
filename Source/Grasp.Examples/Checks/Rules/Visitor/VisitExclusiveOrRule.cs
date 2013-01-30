﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules.Visitor
{
	public class VisitExclusiveOrRule : Behavior
	{
		BinaryRule _exclusiveOrRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_exclusiveOrRule = Rule.ExclusiveOr(Rule.Constant(true), Rule.Constant(true));

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_exclusiveOrRule);
		}

		[Then]
		public void RoutesToVisitBinary()
		{
			Assert.That(_visitor.VisitedRule, Is.EqualTo(_exclusiveOrRule));
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_exclusiveOrRule));
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal BinaryRule VisitedRule { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitBinary(BinaryRule node)
			{
				VisitedRule = node;

				return base.VisitBinary(node);
			}
		}
	}
}