using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class MakeMemberRuleForField : Behavior
	{
		FieldInfo _field;
		Rule _rule;
		MemberRule _memberRule;

		protected override void Given()
		{
			_field = Reflect.Field<TestClass, int>(x => x.TestField);

			_rule = Rule.Constant(true);
		}

		protected override void When()
		{
			_memberRule = Rule.MakeMember(_field, _rule);
		}

		[Then]
		public void HasFieldType()
		{
			Assert.That(_memberRule.Type, Is.EqualTo(RuleType.Field));
		}

		[Then]
		public void HasOriginalField()
		{
			Assert.That(_memberRule.Member, Is.EqualTo(_field));
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_memberRule.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void MemberTypeIsFieldType()
		{
			Assert.That(_memberRule.MemberType, Is.EqualTo(_field.FieldType));
		}

		private sealed class TestClass
		{
			public int TestField = 0;
		}
	}
}