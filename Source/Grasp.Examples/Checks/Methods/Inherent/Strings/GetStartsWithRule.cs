using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Strings
{
	public class GetStartsWithRule : Behavior
	{
		string _value;
		StartsWithMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_value = "Test";

			_method = new StartsWithMethod(_value);

			_expectedMethod = Reflect.Func<ICheckable<string>, string, Check<string>>(Checkable.StartsWith);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(string));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}

		[Then]
		public void HasOneArgument()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(1));
		}

		[Then]
		public void ArgumentIsValue()
		{
			Assert.That(_rule.CheckArguments.Single(), Is.EqualTo(_value));
		}
	}
}