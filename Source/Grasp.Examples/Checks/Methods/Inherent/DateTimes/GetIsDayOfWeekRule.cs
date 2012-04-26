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
	public class GetIsDayOfWeekRule : Behavior
	{
		DayOfWeek _dayOfWeek;
		IsDayOfWeekMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_dayOfWeek = DayOfWeek.Friday;

			_method = new IsDayOfWeekMethod(_dayOfWeek);

			_expectedMethod = Reflect.Func<ICheckable<DateTime>, DayOfWeek, Check<DateTime>>(Checkable.IsDayOfWeek);
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

		[Then]
		public void HasOneArgument()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(1));
		}

		[Then]
		public void ArgumentIsDayOfWeek()
		{
			Assert.That(_rule.CheckArguments.Single(), Is.EqualTo(_dayOfWeek));
		}
	}
}