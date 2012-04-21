using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks
{
	public class ApplyThatCheckWithDefaultResult : Behavior
	{
		bool _defaultResult;
		ICheckable<int> _thatCheck;
		bool _result;

		protected override void Given()
		{
			_defaultResult = false;

			_thatCheck = Check.That(1, _defaultResult);
		}

		protected override void When()
		{
			_result = _thatCheck.Apply();
		}

		[Then]
		public void ResultIsDefault()
		{
			Assert.That(_result, Is.EqualTo(_defaultResult));
		}
	}
}