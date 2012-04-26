using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Numbers.Int64
{
	public class GetIsOddRuleForInt64 : Behavior
	{
		IsOddMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsOddMethod();

			_expectedMethod = Reflect.Func<ICheckable<long>, Check<long>>(Checkable.IsOdd);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(long));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}