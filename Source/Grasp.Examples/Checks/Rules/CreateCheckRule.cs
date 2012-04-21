using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreateCheckRule : Behavior
	{
		MethodInfo _method;
		CheckRule _checkRule;

		protected override void Given()
		{
			_method = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);
		}

		protected override void When()
		{
			_checkRule = Rule.Check(_method);
		}

		[Then]
		public void HasCheckType()
		{
			Assert.That(_checkRule.Type, Is.EqualTo(RuleType.Check));
		}

		[Then]
		public void HasOriginalMethod()
		{
			Assert.That(_checkRule.Method, Is.EqualTo(_method));
		}

		[Then]
		public void HasNoCheckArguments()
		{
			Assert.That(_checkRule.CheckArguments, Is.Empty);
		}
	}
}