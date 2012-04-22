using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Sources
{
	public class GetConditionsForMemberWithMultiple : Behavior
	{
		TestConditionSource _source;
		IEnumerable<Condition> _conditions;

		protected override void Given()
		{
			_source = new TestConditionSource();
		}

		protected override void When()
		{
			_conditions = _source.GetConditions();
		}

		[Then]
		public void ConditionsAreDistinct()
		{
			Assert.That(_conditions.Count(), Is.EqualTo(2));
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
				if(member.DeclaringType != typeof(object))
				{
					var declaration = A.Fake<IConditionDeclaration>();

					A.CallTo(() => declaration.GetConditions(typeof(int))).Returns(new[]
					{
						new Condition<int>(Rule.Constant(true)),
						new Condition<int>(Rule.Constant(false))
					});

					yield return declaration;
				}
			}
		}
	}
}