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
	public class GetStartsWithRuleWithComparisonType : Behavior
	{
		string _value;
		StringComparison _comparisonType;
		StartsWithMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_value = "";
			_comparisonType = StringComparison.InvariantCulture;

			_method = new StartsWithMethod(_value, _comparisonType);

			_expectedMethod = Reflect.Func<ICheckable<string>, string, StringComparison, Check<string>>(Checkable.StartsWith);
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
		public void HasTwoArgument()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(2));
		}

		[Then]
		public void FirstArgumentIsValue()
		{
			Assert.That(_rule.CheckArguments[0], Is.EqualTo(_value));
		}

		[Then]
		public void SecondArgumentIsComparisonType()
		{
			Assert.That(_rule.CheckArguments[1], Is.EqualTo(_comparisonType));
		}
	}
}