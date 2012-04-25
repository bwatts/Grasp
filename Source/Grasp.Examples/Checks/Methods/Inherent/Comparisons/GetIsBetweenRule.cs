using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Comparisons
{
	public class GetIsBetweenRule : Behavior
	{
		int _minimum;
		int _maximum;
		IsBetweenMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_minimum = 1;
			_maximum = 10;

			_method = new IsBetweenMethod(_minimum, _maximum);

			_expectedMethod = Reflect.Func<ICheckable<int>, int, int, Check<int>>(Checkable.IsBetween);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(int));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}

		[Then]
		public void HasTwoArguments()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(2));
		}

		[Then]
		public void FirstArgumentIsMinimum()
		{
			Assert.That(_rule.CheckArguments[0], Is.EqualTo(_minimum));
		}

		[Then]
		public void SecondArgumentIsMaximum()
		{
			Assert.That(_rule.CheckArguments[1], Is.EqualTo(_maximum));
		}
	}
}