using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions
{
	public class CreateTypedConditionWithName : Behavior
	{
		Rule _rule;
		string _name;
		Condition<int> _condition;

		protected override void Given()
		{
			_rule = Rule.Constant(true);

			_name = "Test";
		}

		protected override void When()
		{
			_condition = new Condition<int>(_rule, _name);
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_condition.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void KeyHasTargetTypeAndName()
		{
			Assert.That(_condition.Key, Is.EqualTo(new ConditionKey(typeof(int), _name)));
		}
	}
}