using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions
{
	public class CreateConditionWithTargetTypeAndName : Behavior
	{
		Rule _rule;
		Type _targetType;
		string _name;
		Condition _condition;

		protected override void Given()
		{
			_rule = Rule.Constant(true);

			_targetType = typeof(int);

			_name = "Test";
		}

		protected override void When()
		{
			_condition = new Condition(_rule, _targetType, _name);
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_condition.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void KeyHasTargetTypeAndName()
		{
			Assert.That(_condition.Key, Is.EqualTo(new ConditionKey(_targetType, _name)));
		}
	}
}