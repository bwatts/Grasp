using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class CreateKeyWithName : Behavior
	{
		Type _targetType;
		string _name;
		ConditionKey _key;

		protected override void Given()
		{
			_targetType = typeof(int);
			_name = "Test";
		}

		protected override void When()
		{
			_key = new ConditionKey(_targetType, _name);
		}

		[Then]
		public void HasOriginalTargetType()
		{
			Assert.That(_key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void HasOriginalName()
		{
			Assert.That(_key.Name, Is.EqualTo(_name));
		}

		[Then]
		public void IsNotDefault()
		{
			Assert.That(_key.IsDefault, Is.False);
		}

		[Then]
		public void IsNotInvariant()
		{
			Assert.That(_key.IsInvariant, Is.False);
		}
	}
}