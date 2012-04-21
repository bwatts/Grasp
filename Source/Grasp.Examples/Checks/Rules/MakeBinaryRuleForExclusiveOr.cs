using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class MakeBinaryRuleForExclusiveOr : Behavior
	{
		Rule _left;
		Rule _right;
		BinaryRule _binaryRule;

		protected override void Given()
		{
			_left = Rule.Constant(true);
			_right = Rule.Constant(true);
		}

		protected override void When()
		{
			_binaryRule = Rule.MakeBinary(RuleType.ExclusiveOr, _left, _right);
		}

		[Then]
		public void HasExclusiveOrType()
		{
			Assert.That(_binaryRule.Type, Is.EqualTo(RuleType.ExclusiveOr));
		}

		[Then]
		public void HasOriginalLeft()
		{
			Assert.That(_binaryRule.Left, Is.EqualTo(_left));
		}

		[Then]
		public void HasOriginalRight()
		{
			Assert.That(_binaryRule.Right, Is.EqualTo(_right));
		}
	}
}