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
	public class GetCheckConditionWithName : Behavior
	{
		CheckRule _rule;
		Type _targetType;
		string _name;
		TestCheckAttribute _attribute;
		Condition _condition;

		protected override void Given()
		{
			_rule = Rule.Check(Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive));

			_targetType = typeof(int);

			_name = "Test";

			_attribute = new TestCheckAttribute { Rule = _rule, TargetType = _targetType, Conditions = _name };
		}

		protected override void When()
		{
			_condition = _attribute.GetConditions(_targetType).Single();
		}

		[Then]
		public void HasOriginalTargetType()
		{
			Assert.That(_condition.Key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void HasName()
		{
			Assert.That(_condition.Key.Name, Is.EqualTo(_name));
		}

		[Then]
		public void HasCheckMethodRule()
		{
			Assert.That(_condition.Rule, Is.EqualTo(_rule));
		}

		private sealed class TestCheckAttribute : CheckAttribute
		{
			internal CheckRule Rule { get; set; }

			internal Type TargetType { get; set; }

			public override ICheckMethod GetCheckMethod()
			{
				var checkMethod = A.Fake<ICheckMethod>();

				A.CallTo(() => checkMethod.GetRule(TargetType)).Returns(Rule);

				return checkMethod;
			}
		}
	}
}