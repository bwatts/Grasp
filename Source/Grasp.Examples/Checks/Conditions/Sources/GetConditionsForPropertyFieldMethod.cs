﻿using System;
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
	public class GetConditionsForPropertyFieldMethod : Behavior
	{
		TestConditionSource _source;
		IEnumerable<Condition> _conditions;

		protected override void Given()
		{
			_source = new TestConditionSource();
		}

		protected override void When()
		{
			_conditions = _source.GetConditions().ToList();
		}

		[Then]
		public void IncludesPropertyCondition()
		{
			_conditions.Single(condition => condition.Rule.Type == RuleType.Property);
		}

		[Then]
		public void IncludesFieldCondition()
		{
			_conditions.Single(condition => condition.Rule.Type == RuleType.Field);
		}

		[Then]
		public void IncludesMethodCondition()
		{
			_conditions.Single(condition => condition.Rule.Type == RuleType.Method);
		}

		private sealed class TargetType
		{
			public int TargetField = 1;

			public int TargetProperty { get; set; }

			public int GetTarget()
			{
				return 0;
			}
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

					A.CallTo(() => declaration.GetConditions(typeof(int))).Returns(new[] { new Condition<int>(Rule.Constant(true)) });

					yield return declaration;
				}
			}
		}
	}
}