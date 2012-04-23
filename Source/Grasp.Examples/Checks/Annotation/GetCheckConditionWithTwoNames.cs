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
	public class GetCheckConditionWithTwoNames : Behavior
	{
		CheckRule _rule;
		Type _targetType;
		string _name1;
		string _name2;
		TestCheckAttribute _attribute;
		IDictionary<string, Condition> _conditionsByName;

		protected override void Given()
		{
			_rule = Rule.Check(Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive));

			_targetType = typeof(int);

			_name1 = "Test1";
			_name2 = "Test2";

			_attribute = new TestCheckAttribute { Rule = _rule, TargetType = _targetType, Conditions = _name1 + "," + _name2 };
		}

		protected override void When()
		{
			_conditionsByName = _attribute.GetConditions(_targetType).ToDictionary(condition => condition.Key.Name);
		}

		[Then]
		public void FirstHasOriginalTargetType()
		{
			Assert.That(_conditionsByName[_name1].Key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void FirstHasFirstName()
		{
			Assert.That(_conditionsByName[_name1].Key.Name, Is.EqualTo(_name1));
		}

		[Then]
		public void FirstHasCheckMethodRule()
		{
			Assert.That(_conditionsByName[_name1].Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void SecondHasOriginalTargetType()
		{
			Assert.That(_conditionsByName[_name2].Key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void SecondHasSecondName()
		{
			Assert.That(_conditionsByName[_name2].Key.Name, Is.EqualTo(_name2));
		}

		[Then]
		public void SecondHasCheckMethodRule()
		{
			Assert.That(_conditionsByName[_name2].Rule, Is.EqualTo(_rule));
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