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
	public class CreateCheckRuleWithArguments : Behavior
	{
		string _value;
		MethodInfo _method;
		CheckRule _checkRule;

		protected override void Given()
		{
			_value = "_";

			_method = Reflect.Func<ICheckable<string>, string, Check<string>>(Checkable.StartsWith);
		}

		protected override void When()
		{
			_checkRule = Rule.Check(_method, _value);
		}

		[Then]
		public void HasCheckType()
		{
			Assert.That(_checkRule.Type, Is.EqualTo(RuleType.Check));
		}

		[Then]
		public void HasOriginalMethod()
		{
			Assert.That(_checkRule.Method, Is.EqualTo(_method));
		}

		[Then]
		public void HasOriginalArguments()
		{
			Assert.That(_checkRule.CheckArguments.Single(), Is.EqualTo(_value));
		}
	}
}