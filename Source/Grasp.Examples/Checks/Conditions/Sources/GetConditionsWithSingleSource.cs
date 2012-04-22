using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Sources
{
	public class GetConditionsWithSingleSource : Behavior
	{
		Condition _condition1;
		Condition _condition2;
		CompositeConditionSource _compositeSource;
		IEnumerable<Condition> _conditions;

		protected override void Given()
		{
			_condition1 = new Condition<int>(Rule.Constant(true));
			_condition2 = new Condition<int>(Rule.Constant(true));

			var source = A.Fake<IConditionSource>();

			A.CallTo(() => source.GetConditions()).Returns(new[] { _condition1, _condition2 });

			_compositeSource = new CompositeConditionSource(source);
		}

		protected override void When()
		{
			_conditions = _compositeSource.GetConditions();
		}

		[Then]
		public void ContainsAllConditions()
		{
			var expectedConditions = new[] { _condition1, _condition2 };

			Assert.That(!expectedConditions.Except(_conditions).Any());
		}
	}
}