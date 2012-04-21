using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class GetInvalidName : Behavior
	{
		bool _isAsterisk;

		protected override void Given()
		{}

		protected override void When()
		{
			_isAsterisk = ConditionKey.InvariantName == "*";
		}

		[Then]
		public void IsAsterisk()
		{
			Assert.That(_isAsterisk);
		}
	}
}