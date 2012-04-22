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
	public class GetConditionWithOtherKeys : Behavior
	{
		Condition _condition;
		ConditionRepository _repository;
		Condition _repositoryCondition;

		protected override void Given()
		{
			_condition = new Condition<int>(Rule.Constant(true));

			var otherCondition = new Condition<int>(Rule.Constant(true), "Other");

			var source = A.Fake<IConditionSource>();

			A.CallTo(() => source.GetConditions()).Returns(new[] { _condition, otherCondition });

			_repository = new ConditionRepository(source);
		}

		protected override void When()
		{
			_repositoryCondition = _repository.GetCondition(_condition.Key);
		}

		[Then]
		public void IsOriginalCondition()
		{
			Assert.That(_repositoryCondition, Is.EqualTo(_condition));
		}
	}
}