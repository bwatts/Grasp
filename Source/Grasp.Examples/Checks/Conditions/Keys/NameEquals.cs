using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class NameEquals : Behavior
	{
		ConditionKey _key;
		string _name;
		bool _namesAreEqual;

		protected override void Given()
		{
			_name = "Test";

			_key = new ConditionKey(typeof(int), _name);
		}

		protected override void When()
		{
			_namesAreEqual = _key.NameEquals(_name);
		}

		[Then]
		public void NamesAreEqual()
		{
			Assert.That(_namesAreEqual);
		}
	}
}