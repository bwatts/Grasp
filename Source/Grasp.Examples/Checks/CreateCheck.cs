using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks
{
	public class CreateCheck : Behavior
	{
		int _target;
		TestCheck _check;

		protected override void Given()
		{
			_target = 1;
		}

		protected override void When()
		{
			_check = new TestCheck(_target);
		}

		[Then]
		public void HasOriginalTarget()
		{
			Assert.That(_check.Target, Is.EqualTo(_target));
		}

		public void HasTargetType()
		{
			Assert.That(_check.TargetType, Is.EqualTo(_target.GetType()));
		}

		private sealed class TestCheck : Check<int>
		{
			internal TestCheck(int target) : base(target)
			{}

			public override bool Apply()
			{
				return false;
			}
		}
	}
}