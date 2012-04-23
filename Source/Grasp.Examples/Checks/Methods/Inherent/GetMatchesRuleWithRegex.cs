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

namespace Grasp.Checks.Methods.Inherent
{
	public class GetMatchesRuleWithRegex : Behavior
	{
		Regex _regex;
		MatchesMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_regex = new Regex("");

			_method = new MatchesMethod(_regex);

			_expectedMethod = Reflect.Func<ICheckable<string>, Regex, Check<string>>(Checkable.Matches);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(string));
		}

		[Then]
		public void HasMatchesMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}

		[Then]
		public void HasOneArgument()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(1));
		}

		[Then]
		public void ArgumentIsRegex()
		{
			Assert.That(_rule.CheckArguments.Single(), Is.EqualTo(_regex));
		}
	}
}