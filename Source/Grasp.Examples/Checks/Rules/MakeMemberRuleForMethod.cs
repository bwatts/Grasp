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
	public class MakeMemberRuleForMethod : Behavior
	{
		MethodInfo _method;
		Rule _rule;
		MemberRule _memberRule;

		protected override void Given()
		{
			_method = Reflect.Func<TestClass, int>(x => x.TestMethod());

			_rule = Rule.Constant(true);
		}

		protected override void When()
		{
			_memberRule = Rule.MakeMember(_method, _rule);
		}

		[Then]
		public void HasMethodType()
		{
			Assert.That(_memberRule.Type, Is.EqualTo(RuleType.Method));
		}

		[Then]
		public void HasOriginalMethod()
		{
			Assert.That(_memberRule.Member, Is.EqualTo(_method));
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_memberRule.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void MemberTypeIsReturnType()
		{
			Assert.That(_memberRule.MemberType, Is.EqualTo(_method.ReturnType));
		}

		private sealed class TestClass
		{
			public int TestMethod()
			{
				return 0;
			}
		}
	}
}