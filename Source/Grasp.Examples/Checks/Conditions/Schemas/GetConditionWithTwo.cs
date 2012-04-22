using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Schemas
{
	public class GetConditionWithTwo : Behavior
	{
		Type _targetType;
		Rule _rule1;
		Rule _rule2;
		ConditionRepository _repository;
		Condition _condition;

		protected override void Given()
		{
			_targetType = typeof(int);

			_rule1 = Rule.Constant(true);
			_rule2 = Rule.Constant(true);

			var condition1 = new Condition(_rule1, _targetType);
			var condition2 = new Condition(_rule2, _targetType);

			var source = A.Fake<IConditionSource>();

			A.CallTo(() => source.GetConditions()).Returns(new[] { condition1, condition2 });

			_repository = new ConditionRepository(source);
		}

		protected override void When()
		{
			_condition = _repository.GetCondition(new ConditionKey(_targetType));
		}

		[Then]
		public void RuleHasAndType()
		{
			Assert.That(_condition.Rule.Type, Is.EqualTo(RuleType.And));
		}

		[Then]
		public void FirstRuleIsLeftAndOperand()
		{
			var andRule = (BinaryRule) _condition.Rule;

			Assert.That(andRule.Left, Is.EqualTo(_rule1));
		}

		[Then]
		public void SecondRuleIsRightAndOperand()
		{
			var andRule = (BinaryRule) _condition.Rule;

			Assert.That(andRule.Right, Is.EqualTo(_rule2));
		}
	}
}