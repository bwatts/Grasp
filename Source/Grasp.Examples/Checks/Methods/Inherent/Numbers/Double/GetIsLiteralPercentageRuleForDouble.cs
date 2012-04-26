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
	public class GetIsLiteralPercentageRuleForDouble : Behavior
	{
		IsLiteralPercentageMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsLiteralPercentageMethod();

			_expectedMethod = Reflect.Func<ICheckable<double>, Check<double>>(Checkable.IsLiteralPercentage);
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