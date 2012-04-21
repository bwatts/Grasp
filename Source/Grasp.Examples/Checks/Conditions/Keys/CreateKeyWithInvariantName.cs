using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class CreateKeyWithInvariantName : Behavior
	{
		Type _targetType;
		ConditionKey _key;

		protected override void Given()
		{
			_targetType = typeof(int);
		}

		protected override void When()
		{
			_key = new ConditionKey(_targetType, ConditionKey.InvariantName);
		}

		[Then]
		public void IsNotDefault()
		{
			Assert.That(_key.IsDefault, Is.False);
		}

		[Then]
		public void IsInvariant()
		{
			Assert.That(_key.IsInvariant);
		}
	}
}