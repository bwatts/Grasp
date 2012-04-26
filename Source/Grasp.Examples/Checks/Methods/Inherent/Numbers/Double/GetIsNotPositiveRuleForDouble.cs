using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Numbers.Double
{
	public class GetIsNotPositiveRuleForDouble : Behavior
	{
		IsNotPositiveMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsNotPositiveMethod();

			_expectedMethod = Reflect.Func<ICheckable<double>, Check<double>>(Checkable.IsNotPositive);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(double));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}