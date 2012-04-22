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
	public class GetCheckConditionWithRepeatedName : Behavior
	{
		CheckRule _rule;
		Type _targetType;
		string _name;
		TestCheckAttribute _attribute;
		IEnumerable<Condition> _conditions;

		protected override void Given()
		{
			_rule = Rule.Check(Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive));

			_targetType = typeof(int);

			_name = "Test";

			_attribute = new TestCheckAttribute { Rule = _rule, TargetType = _targetType, Conditions = _name + "," + _name };
		}

		protected override void When()
		{
			_conditions = _attribute.GetConditions(_targetType);
		}

		[Then]
		public void DoesNotRepeatCondition()
		{
			Assert.That(_conditions.Count(), Is.EqualTo(1));
		}

		[Then]
		public void HasName()
		{
			Assert.That(_conditions.Single().Key.Name, Is.EqualTo(_name));
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