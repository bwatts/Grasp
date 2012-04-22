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
	public class GetConditionsWithMultipleSources : Behavior
	{
		Condition _condition1;
		Condition _condition2;
		Condition _condition3;
		Condition _condition4;
		CompositeConditionSource _compositeSource;
		IEnumerable<Condition> _conditions;

		protected override void Given()
		{
			_condition1 = new Condition<int>(Rule.Constant(true));
			_condition2 = new Condition<int>(Rule.Constant(true));
			_condition3 = new Condition<int>(Rule.Constant(true));
			_condition4 = new Condition<int>(Rule.Constant(true));

			var source1 = A.Fake<IConditionSource>();
			var source2 = A.Fake<IConditionSource>();

			A.CallTo(() => source1.GetConditions()).Returns(new[] { _condition1, _condition2 });
			A.CallTo(() => source2.GetConditions()).Returns(new[] { _condition3, _condition4 });

			_compositeSource = new CompositeConditionSource(source1, source2);
		}

		protected override void When()
		{
			_conditions = _compositeSource.GetConditions();
		}

		[Then]
		public void ContainsAllConditions()
		{
			var expectedConditions = new[] { _condition1, _condition2, _condition3, _condition4 };

			Assert.That(!expectedConditions.Except(_conditions).Any());
		}
	}
}