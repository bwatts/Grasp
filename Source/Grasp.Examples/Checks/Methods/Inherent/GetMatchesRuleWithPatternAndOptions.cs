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
	public class GetMatchesRuleWithPatternAndOptions : Behavior
	{
		string _pattern;
		RegexOptions _options;
		MatchesMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_pattern = "";
			_options = default(RegexOptions);

			_method = new MatchesMethod(_pattern, _options);

			_expectedMethod = Reflect.Func<ICheckable<string>, string, RegexOptions, Check<string>>(Checkable.Matches);
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
		public void HasTwoArguments()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(2));
		}

		[Then]
		public void FirstArgumentIsPattern()
		{
			Assert.That(_rule.CheckArguments[0], Is.EqualTo(_pattern));
		}

		[Then]
		public void SecondArgumentIsOptions()
		{
			Assert.That(_rule.CheckArguments[1], Is.EqualTo(_options));
		}
	}
}