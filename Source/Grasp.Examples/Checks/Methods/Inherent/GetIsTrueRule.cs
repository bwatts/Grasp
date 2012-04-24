using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent
{
	public class GetIsTrueRule : Behavior
	{
		IsTrueMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsTrueMethod();

			_expectedMethod = Reflect.Func<ICheckable<bool>, Check<bool>>(Checkable.IsTrue);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(bool));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}