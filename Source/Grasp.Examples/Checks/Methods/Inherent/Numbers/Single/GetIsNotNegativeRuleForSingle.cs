using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Numbers.Single
{
	public class GetIsNotNegativeRuleForSingle : Behavior
	{
		IsNotNegativeMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsNotNegativeMethod();

			_expectedMethod = Reflect.Func<ICheckable<float>, Check<float>>(Checkable.IsNotNegative);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(float));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}