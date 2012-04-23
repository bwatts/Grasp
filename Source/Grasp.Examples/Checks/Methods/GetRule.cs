using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods
{
	public class GetRule : Behavior
	{
		MethodInfo _method;
		TestCheckMethod _checkMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);

			_checkMethod = new TestCheckMethod { Method = _method };
		}

		protected override void When()
		{
			_rule = _checkMethod.GetRule(typeof(int));
		}

		[Then]
		public void AppliesCheck()
		{
			Assert.That(_rule.Method, Is.EqualTo(_method));
		}

		[Then]
		public void HasNoArguments()
		{
			Assert.That(_rule.CheckArguments.Any(), Is.False);
		}

		private sealed class TestCheckMethod : CheckMethod
		{
			internal MethodInfo Method { get; set; }

			protected override bool SupportsTargetType(Type targetType)
			{
				return true;
			}

			protected override MethodInfo GetMethod(Type targetType, Type checkType)
			{
				return Method;
			}
		}
	}
}