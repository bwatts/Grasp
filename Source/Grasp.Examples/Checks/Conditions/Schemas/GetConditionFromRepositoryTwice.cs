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
	public class GetConditionFromRepositoryTwice : Behavior
	{
		Condition _condition;
		IConditionSource _source;
		ConditionRepository _repository;
		Condition _firstCondition;
		Condition _secondCondition;

		protected override void Given()
		{
			_condition = new Condition<int>(Rule.Constant(true));

			_source = A.Fake<IConditionSource>();

			A.CallTo(() => _source.GetConditions()).Returns(new[] { _condition });

			_repository = new ConditionRepository(_source);

			_firstCondition = _repository.GetCondition(_condition.Key);
		}

		protected override void When()
		{
			_secondCondition = _repository.GetCondition(_condition.Key);
		}

		[Then]
		public void FirstConditionIsOriginal()
		{
			Assert.That(_firstCondition, Is.EqualTo(_condition));
		}

		[Then]
		public void SecondConditionIsOriginal()
		{
			Assert.That(_secondCondition, Is.EqualTo(_condition));
		}

		[Then]
		public void SourceIsOnlyInvokedOnce()
		{
			A.CallTo(() => _source.GetConditions()).MustHaveHappened(Repeated.Exactly.Once);
		}
	}
}