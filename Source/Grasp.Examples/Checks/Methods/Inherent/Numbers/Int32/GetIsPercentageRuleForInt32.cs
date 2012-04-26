using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Numbers.Int32
{
	public class GetIsPercentageRuleForInt32 : Behavior
	{
		IsPercentageMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsPercentageMethod();

			_expectedMethod = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPercentage);
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
	}
}