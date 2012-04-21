using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks
{
	public class ApplyThatCheck : Behavior
	{
		ICheckable<int> _thatCheck;
		bool _result;

		protected override void Given()
		{
			_thatCheck = Check.That(1);
		}

		protected override void When()
		{
			_result = _thatCheck.Apply();
		}

		[Then]
		public void IsTrue()
		{
			Assert.That(_result, Is.True);
		}
	}
}