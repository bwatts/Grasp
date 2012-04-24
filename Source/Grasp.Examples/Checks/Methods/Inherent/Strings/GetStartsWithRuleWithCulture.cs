using System;
using System.Collections.Generic;
using System.Globalization;
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
	public class GetStartsWithRuleWithCulture : Behavior
	{
		string _value;
		bool _ignoreCase;
		CultureInfo _culture;
		StartsWithMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_value = "Test";
			_ignoreCase = true;
			_culture = CultureInfo.InvariantCulture;

			_method = new StartsWithMethod(_value, _ignoreCase, _culture);

			_expectedMethod = Reflect.Func<ICheckable<string>, string, bool, CultureInfo, Check<string>>(Checkable.StartsWith);
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
		public void HasThreeArgument()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(3));
		}

		[Then]
		public void FirstArgumentIsValue()
		{
			Assert.That(_rule.CheckArguments[0], Is.EqualTo(_value));
		}

		[Then]
		public void SecondArgumentIsIgnoreCase()
		{
			Assert.That(_rule.CheckArguments[1], Is.EqualTo(_ignoreCase));
		}

		[Then]
		public void ThirdArgumentIsCulture()
		{
			Assert.That(_rule.CheckArguments[2], Is.EqualTo(_culture));
		}
	}
}