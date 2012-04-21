using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class NameEqualsDifferentCase : Behavior
	{
		ConditionKey _key;
		bool _namesAreEqual;

		protected override void Given()
		{
			_key = new ConditionKey(typeof(int), "Test");
		}

		protected override void When()
		{
			_namesAreEqual = _key.NameEquals("test");
		}

		[Then]
		public void NamesAreNotEqual()
		{
			Assert.That(_namesAreEqual, Is.False);
		}
	}
}