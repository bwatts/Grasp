using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Characters
{
	public class GetIsSurrogateRule : Behavior
	{
		IsSurrogateMethod _method;
		MethodInfo _expectedMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = new IsSurrogateMethod();

			_expectedMethod = Reflect.Func<ICheckable<char>, Check<char>>(Checkable.IsSurrogate);
		}

		protected override void When()
		{
			_rule = _method.GetRule(typeof(char));
		}

		[Then]
		public void HasIsNullMethod()
		{
			Assert.That(_rule.Method, Is.EqualTo(_expectedMethod));
		}
	}
}