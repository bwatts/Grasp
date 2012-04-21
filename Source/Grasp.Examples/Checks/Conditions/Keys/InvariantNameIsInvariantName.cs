using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class InvariantNameIsInvariantName : Behavior
	{
		bool _isInvariantName;

		protected override void Given()
		{}

		protected override void When()
		{
			_isInvariantName = ConditionKey.IsInvariantName(ConditionKey.InvariantName);
		}

		[Then]
		public void IsTrue()
		{
			Assert.That(_isInvariantName);
		}
	}
}