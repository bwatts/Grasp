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
		IsEqualToMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsEqualToMethod(1);

			_expectedMethod = Reflect.Func<ICheckable<int>, int, Check<int>>(Checkable.IsEqualTo);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(int));
		}

		[Then]
		public void HasIsNullMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}