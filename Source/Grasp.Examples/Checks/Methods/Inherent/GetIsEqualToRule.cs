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
	public class GetIsEqualToRule : Behavior
	{
		int _value;
		IsEqualToMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_value = 1;

			_method = new IsEqualToMethod(_value);

			_expectedMethod = Reflect.Func<ICheckable<int>, int, Check<int>>(Checkable.IsEqualTo);
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

		[Then]
		public void HasOneArgument()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(1));
		}

		[Then]
		public void ArgumentIsValue()
		{
			Assert.That(_rule.CheckArguments.Single(), Is.EqualTo(_value));
		}
	}
}