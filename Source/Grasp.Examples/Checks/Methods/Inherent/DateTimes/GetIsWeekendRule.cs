using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.DateTimes
{
	public class GetIsWeekendRule : Behavior
	{
		IsWeekendMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsWeekendMethod();

			_expectedMethod = Reflect.Func<ICheckable<DateTime>, Check<DateTime>>(Checkable.IsWeekend);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(DateTime));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}