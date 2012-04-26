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
	public class GetIsPositiveRuleForInt32 : Behavior
	{
		IsPositiveMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsPositiveMethod();

			_expectedMethod = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);
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