using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class GetNameEqualityComparer : Behavior
	{
		bool _isInvariantCulture;

		protected override void Given()
		{}

		protected override void When()
		{
			_isInvariantCulture = ConditionKey.NameEqualityComparer == StringComparer.InvariantCulture;
		}

		[Then]
		public void IsInvariantCulture()
		{
			Assert.That(_isInvariantCulture);
		}
	}
}