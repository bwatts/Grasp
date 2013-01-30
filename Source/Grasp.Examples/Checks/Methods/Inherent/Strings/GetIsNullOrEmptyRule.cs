﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Strings
{
	public class GetIsNullOrEmptyRule : Behavior
	{
		IsNullOrEmptyMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsNullOrEmptyMethod();

			_expectedMethod = Reflect.Func<ICheckable<string>, Check<string>>(Checkable.IsNullOrEmpty);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(string));
		}

		[Then]
		public void HasExpectedMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}