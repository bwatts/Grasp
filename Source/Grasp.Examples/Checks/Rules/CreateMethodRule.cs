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
	public class CreateMethodRule : Behavior
	{
		MethodInfo _method;
		Rule _rule;
		MemberRule _methodRule;

		protected override void Given()
		{
			_method = Reflect.Func<TestClass, int>(x => x.TestMethod());

			_rule = Rule.Constant(true);
		}

		protected override void When()
		{
			_methodRule = Rule.Method(_method, _rule);
		}

		[Then]
		public void HasMethodType()
		{
			Assert.That(_methodRule.Type, Is.EqualTo(RuleType.Method));
		}

		[Then]
		public void HasOriginalMethod()
		{
			Assert.That(_methodRule.Member, Is.EqualTo(_method));
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_methodRule.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void MemberTypeIsReturnType()
		{
			Assert.That(_methodRule.MemberType, Is.EqualTo(_method.ReturnType));
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