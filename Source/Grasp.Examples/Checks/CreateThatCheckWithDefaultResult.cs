using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks
{
	public class CreateThatCheckWithDefaultResult : Behavior
	{
		int _target;
		bool _defaultResult;
		ICheckable<int> _thatCheck;

		protected override void Given()
		{
			_target = 1;
			_defaultResult = false;
		}

		protected override void When()
		{
			_thatCheck = Check.That(_target, _defaultResult);
		}

		[Then]
		public void IsNotNull()
		{
			Assert.That(_thatCheck, Is.Not.Null);
		}

		[Then]
		public void HasOriginalTarget()
		{
			Assert.That(_thatCheck.Target, Is.EqualTo(_target));
		}

		[Then]
		public void HasTargetType()
		{
			Assert.That(_thatCheck.TargetType, Is.EqualTo(_target.GetType()));
		}
	}
}