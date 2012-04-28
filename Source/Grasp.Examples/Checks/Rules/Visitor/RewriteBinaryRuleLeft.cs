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
	public class RewriteBinaryRuleLeft : Behavior
	{
		Rule _left;
		Rule _newLeft;
		BinaryRule _binaryRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_left = Rule.Constant(true);

			_binaryRule = Rule.And(_left, Rule.Constant(true));

			_newLeft = Rule.Constant(false);

			_visitor = new TestRuleVisitor { Left = _left, NewLeft = _newLeft };
		}

		protected override void When()
		{
			_resultRule = _visitor.VisitRule(_binaryRule);
		}

		[Then]
		public void ResultHasOriginalType()
		{
			Assert.That(_resultRule.Type, Is.EqualTo(_binaryRule.Type));
		}

		[Then]
		public void LeftIsNew()
		{
			var resultBinaryRule = (BinaryRule) _resultRule;

			Assert.That(resultBinaryRule.Left, Is.EqualTo(_newLeft));
		}

		[Then]
		public void RightIsOriginal()
		{
			var resultBinaryRule = (BinaryRule) _resultRule;

			Assert.That(resultBinaryRule.Right, Is.EqualTo(_binaryRule.Right));
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal Rule Left { get; set; }

			internal Rule NewLeft { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitConstant(ConstantRule node)
			{
				return node == Left ? NewLeft : node;
			}
		}
	}
}