using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using FakeItEasy;
using Grasp.Checks.Conditions;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Annotation
{
	public class GetCheckConditionWithNameWithSpaces : Behavior
	{
		Type _targetType;
		string _name;
		TestCheckAttribute _attribute;
		Condition _condition;

		protected override void Given()
		{
			var rule = Rule.Check(Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive));

			_targetType = typeof(int);

			_name = " Test ";

			_attribute = new TestCheckAttribute { Rule = rule, TargetType = _targetType, Conditions = _name };
		}

		protected override void When()
		{
			_condition = _attribute.GetConditions(_targetType).Single();
		}

		[Then]
		public void PreservesSpacesInName()
		{
			// In the absence of better-defined scenarios, it is easiest to preserve names exactly as specified. Subject to change.

			Assert.That(_condition.Key.Name, Is.EqualTo(_name));
		}

		private sealed class TestCheckAttribute : CheckAttribute
		{
			internal CheckRule Rule { get; set; }

			internal Type TargetType { get; set; }

			protected override ICheckMethod GetCheckMethod()
			{
				var checkMethod = A.Fake<ICheckMethod>();

				A.CallTo(() => checkMethod.GetRule(TargetType)).Returns(Rule);

				return checkMethod;
			}
		}
	}
}