using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks
{
	public class ImplicitlyConvertCheckToBoolean : Behavior
	{
		TestCheck _check;
		bool _result;

		protected override void Given()
		{
			_check = new TestCheck();
		}

		protected override void When()
		{
			_result = _check;
		}

		[Then]
		public void IsTrue()
		{
			Assert.That(_result, Is.EqualTo(_check.Result));
		}

		private sealed class TestCheck : Check<int>
		{
			internal TestCheck() : base(0)
			{}

			internal bool Result { get; set; }

			public override bool Apply()
			{
				return Result;
			}
		}
	}
}