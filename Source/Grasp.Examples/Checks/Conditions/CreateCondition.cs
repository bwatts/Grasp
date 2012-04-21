using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions
{
	public class CreateCondition : Behavior
	{
		Rule _rule;
		ConditionKey _key;
		Condition _condition;

		protected override void Given()
		{
			_rule = Rule.Constant(true);

			_key = new ConditionKey(typeof(int));
		}

		protected override void When()
		{
			_condition = new Condition(_rule, _key);
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_condition.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void HasOriginalKey()
		{
			Assert.That(_condition.Key, Is.EqualTo(_key));
		}
	}
}