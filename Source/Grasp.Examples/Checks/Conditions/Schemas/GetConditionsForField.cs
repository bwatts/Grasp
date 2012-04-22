using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Schemas
{
	public class GetConditionsForField : Behavior
	{
		TestConditionSource _source;
		Condition _condition;

		protected override void Given()
		{
			_source = new TestConditionSource();
		}

		protected override void When()
		{
			_condition = _source.GetConditions().Single();
		}

		[Then]
		public void KeyHasTargetType()
		{
			Assert.That(_condition.Key.TargetType, Is.EqualTo(_source.TargetType));
		}

		[Then]
		public void KeyHasOriginalName()
		{
			Assert.That(_condition.Key.Name, Is.EqualTo(ConditionKey.DefaultName));
		}

		[Then]
		public void RuleHasFieldType()
		{
			Assert.That(_condition.Rule.Type, Is.EqualTo(RuleType.Field));
		}

		[Then]
		public void RuleAppliesToField()
		{
			var fieldRule = (MemberRule) _condition.Rule;

			Assert.That(fieldRule.Member, Is.EqualTo(typeof(TargetType).GetField("Target")));
		}

		[Then]
		public void FieldRuleAppliesRule()
		{
			var fieldRule = (MemberRule) _condition.Rule;
			var passesRule = (ConstantRule) fieldRule.Rule;

			Assert.That(passesRule.Passes);
		}

		private sealed class TargetType
		{
			public int Target = 0;
		}

		private sealed class TestConditionSource : MemberConditionSource
		{
			public override Type TargetType
			{
				get { return typeof(TargetType); }
			}

			protected override IEnumerable<IConditionDeclaration> GetDeclarations(MemberInfo member)
			{
				if(member == typeof(TargetType).GetField("Target"))
				{
					var declaration = A.Fake<IConditionDeclaration>();

					A.CallTo(() => declaration.GetConditions(typeof(int))).Returns(new[] { new Condition<int>(Rule.Constant(true)) });

					yield return declaration;
				}
			}
		}
	}
}