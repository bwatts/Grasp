using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreateExclusiveOrRule : Behavior
	{
		Rule _left;
		Rule _right;
		BinaryRule _exclusiveOrRule;

		protected override void Given()
		{
			_left = Rule.Constant(true);
			_right = Rule.Constant(true);
		}

		protected override void When()
		{
			_exclusiveOrRule = Rule.ExclusiveOr(_left, _right);
		}

		[Then]
		public void HasExclusiveOrType()
		{
			Assert.That(_exclusiveOrRule.Type, Is.EqualTo(RuleType.ExclusiveOr));
		}

		[Then]
		public void HasOriginalLeft()
		{
			Assert.That(_exclusiveOrRule.Left, Is.EqualTo(_left));
		}

		[Then]
		public void HasOriginalRight()
		{
			Assert.That(_exclusiveOrRule.Right, Is.EqualTo(_right));
		}
	}
}