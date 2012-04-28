using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules.Visitor
{
	public class VisitLambdaRule : Behavior
	{
		LambdaRule _lambdaRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_lambdaRule = Rule.Lambda(Expression.Lambda<Func<int, bool>>(Expression.Constant(true), Expression.Parameter(typeof(int))));

			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_lambdaRule);
		}

		[Then]
		public void RoutesToVisitLambda()
		{
			Assert.That(_visitor.VisitedRule, Is.EqualTo(_lambdaRule));
		}

		[Then]
		public void ResultIsOriginal()
		{
			Assert.That(_resultRule, Is.EqualTo(_lambdaRule));
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal LambdaRule VisitedRule { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitLambda(LambdaRule node)
			{
				VisitedRule = node;

				return base.VisitLambda(node);
			}
		}
	}
}