using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class CreateKey : Behavior
	{
		Type _targetType;
		ConditionKey _key;

		protected override void Given()
		{
			_targetType = typeof(int);
		}

		protected override void When()
		{
			_key = new ConditionKey(_targetType);
		}

		[Then]
		public void HasOriginalTargetType()
		{
			Assert.That(_key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void NameIsDefault()
		{
			Assert.That(ConditionKey.IsDefaultName(_key.Name));
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