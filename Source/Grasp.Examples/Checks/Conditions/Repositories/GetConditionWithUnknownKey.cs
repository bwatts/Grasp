using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Repositories
{
	public class GetConditionWithUnknownKey : Behavior
	{
		ConditionRepository _repository;
		Condition _condition;

		protected override void Given()
		{
			var condition = new Condition<int>(Rule.Constant(true));

			var source = A.Fake<IConditionSource>();

			A.CallTo(() => source.GetConditions()).Returns(new[] { condition });

			_repository = new ConditionRepository(source);
		}

		protected override void When()
		{
			_condition = _repository.GetCondition(new ConditionKey(typeof(int), "Unknown"));
		}

		[Then]
		public void IsNull()
		{
			Assert.That(_condition, Is.Null);
		}
	}
}