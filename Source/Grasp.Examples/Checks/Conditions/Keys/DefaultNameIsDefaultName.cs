using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class DefaultNameIsDefaultName : Behavior
	{
		bool _isDefaultName;

		protected override void Given()
		{}

		protected override void When()
		{
			_isDefaultName = ConditionKey.IsDefaultName(ConditionKey.DefaultName);
		}

		[Then]
		public void IsTrue()
		{
			Assert.That(_isDefaultName);
		}
	}
}