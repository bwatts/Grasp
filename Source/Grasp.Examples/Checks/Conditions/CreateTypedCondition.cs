using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions
{
	public class CreateTypedCondition : Behavior
	{
		Rule _rule;
		Condition<int> _condition;

		protected override void Given()
		{
			_rule = Rule.Constant(true);
		}

		protected override void When()
		{
			_condition = new Condition<int>(_rule);
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_condition.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void KeyHasTargetType()
		{
			Assert.That(_condition.Key, Is.EqualTo(new ConditionKey(typeof(int))));
		}
	}
}