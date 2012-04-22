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
	public class GetConditionsForProperty : Behavior
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
		public void RuleHasPropertyType()
		{
			Assert.That(_condition.Rule.Type, Is.EqualTo(RuleType.Property));
		}

		[Then]
		public void RuleAppliesToProperty()
		{
			var propertyRule = (MemberRule) _condition.Rule;

			Assert.That(propertyRule.Member, Is.EqualTo(typeof(TargetType).GetProperty("Target")));
		}

		[Then]
		public void FieldRuleAppliesRule()
		{
			var propertyRule = (MemberRule) _condition.Rule;
			var passesRule = (ConstantRule) propertyRule.Rule;

			Assert.That(passesRule.Passes);
		}

		private sealed class TargetType
		{
			public int Target { get; set; }
		}

		private sealed class TestConditionSource : MemberConditionSource
		{
			public override Type TargetType
			{
				get { return typeof(TargetType); }
			}

			protected override IEnumerable<IConditionDeclaration> GetDeclarations(MemberInfo member)
			{
				if(member == typeof(TargetType).GetProperty("Target"))
				{
					var declaration = A.Fake<IConditionDeclaration>();

					A.CallTo(() => declaration.GetConditions(typeof(int))).Returns(new[] { new Condition<int>(Rule.Constant(true)) });

					yield return declaration;
				}
			}
		}
	}
}