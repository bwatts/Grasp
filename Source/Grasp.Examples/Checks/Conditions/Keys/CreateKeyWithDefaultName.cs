using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class CreateKeyWithDefaultName : Behavior
	{
		Type _targetType;
		ConditionKey _key;

		protected override void Given()
		{
			_targetType = typeof(int);
		}

		protected override void When()
		{
			_key = new ConditionKey(_targetType, ConditionKey.DefaultName);
		}

		[Then]
		public void IsDefault()
		{
			Assert.That(_key.IsDefault);
		}

		[Then]
		public void IsNotInvariant()
		{
			Assert.That(_key.IsInvariant, Is.False);
		}
	}
}