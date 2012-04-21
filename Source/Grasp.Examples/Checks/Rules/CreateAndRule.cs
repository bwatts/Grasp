using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreateAndRule : Behavior
	{
		Rule _left;
		Rule _right;
		BinaryRule _andRule;

		protected override void Given()
		{
			_left = Rule.Constant(true);
			_right = Rule.Constant(true);
		}

		protected override void When()
		{
			_andRule = Rule.And(_left, _right);
		}

		[Then]
		public void HasAndType()
		{
			Assert.That(_andRule.Type, Is.EqualTo(RuleType.And));
		}

		[Then]
		public void HasOriginalLeft()
		{
			Assert.That(_andRule.Left, Is.EqualTo(_left));
		}

		[Then]
		public void HasOriginalRight()
		{
			Assert.That(_andRule.Right, Is.EqualTo(_right));
		}
	}
}