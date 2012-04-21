using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class KeysEqualWithDifferentName : Behavior
	{
		ConditionKey _key1;
		ConditionKey _key2;
		bool _equal;

		protected override void Given()
		{
			_key1 = new ConditionKey(typeof(int), "Test1");
			_key2 = new ConditionKey(typeof(int), "Test2");
		}

		protected override void When()
		{
			_equal = _key1.Equals(_key2);
		}

		[Then]
		public void KeysAreNotEqual()
		{
			Assert.That(_equal, Is.False);
		}
	}
}