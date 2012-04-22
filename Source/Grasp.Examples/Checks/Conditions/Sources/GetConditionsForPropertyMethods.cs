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
	public class GetConditionsForPropertyMethods : Behavior
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
		public void NoConditionsDeclared()
		{
			Assert.That(!_conditions.Any());
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
				// Getters and setters should be ignored even though they are methods. These declarations should never be used, resulting in an empty set of conditions.

				if(member != typeof(TargetType).GetProperty("Target") && member.DeclaringType != typeof(object))
				{
					var declaration = A.Fake<IConditionDeclaration>();

					A.CallTo(() => declaration.GetConditions(typeof(int))).Returns(new[] { new Condition<int>(Rule.Constant(true)) });

					yield return declaration;
				}
			}
		}
	}
}