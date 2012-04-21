using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreateOrRule : Behavior
	{
		Rule _left;
		Rule _right;
		BinaryRule _orRule;

		protected override void Given()
		{
			_left = Rule.Constant(true);
			_right = Rule.Constant(true);
		}

		protected override void When()
		{
			_orRule = Rule.Or(_left, _right);
		}

		[Then]
		public void HasOrType()
		{
			Assert.That(_orRule.Type, Is.EqualTo(RuleType.Or));
		}

		[Then]
		public void HasOriginalLeft()
		{
			Assert.That(_orRule.Left, Is.EqualTo(_left));
		}

		[Then]
		public void HasOriginalRight()
		{
			Assert.That(_orRule.Right, Is.EqualTo(_right));
		}
	}
}