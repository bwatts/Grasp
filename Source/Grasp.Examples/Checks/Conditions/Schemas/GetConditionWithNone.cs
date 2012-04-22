using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Schemas
{
	public class GetConditionWithNone : Behavior
	{
		ConditionRepository _repository;
		Condition _condition;

		protected override void Given()
		{
			var source = A.Fake<IConditionSource>();

			A.CallTo(() => source.GetConditions()).Returns(Enumerable.Empty<Condition>());

			_repository = new ConditionRepository(source);
		}

		protected override void When()
		{
			_condition = _repository.GetCondition(new ConditionKey(typeof(int)));
		}

		[Then]
		public void IsNull()
		{
			Assert.That(_condition, Is.Null);
		}
	}
}