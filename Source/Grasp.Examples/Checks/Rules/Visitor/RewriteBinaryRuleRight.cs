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
	public class RewriteBinaryRuleRight : Behavior
	{
		Rule _right;
		Rule _newRight;
		BinaryRule _binaryRule;
		TestRuleVisitor _visitor;
		Rule _resultRule;

		protected override void Given()
		{
			_right = Rule.Constant(true);

			_binaryRule = Rule.And(Rule.Constant(true), _right);

			_newRight = Rule.Constant(false);

			_visitor = new TestRuleVisitor { Right = _right, NewRight = _newRight };
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
		public void LeftIsOriginal()
		{
			var resultBinaryRule = (BinaryRule) _resultRule;

			Assert.That(resultBinaryRule.Left, Is.EqualTo(_binaryRule.Left));
		}

		[Then]
		public void RightIsNew()
		{
			var resultBinaryRule = (BinaryRule) _resultRule;

			Assert.That(resultBinaryRule.Right, Is.EqualTo(_newRight));
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal Rule Right { get; set; }

			internal Rule NewRight { get; set; }

			internal Rule VisitRule(Rule rule)
			{
				return Visit(rule);
			}

			protected override Rule VisitConstant(ConstantRule node)
			{
				return node == Right ? NewRight : node;
			}
		}
	}
}