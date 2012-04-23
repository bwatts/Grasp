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
	public class GetRuleWithOneArgument : Behavior
	{
		MethodInfo _method;
		int _argument;
		TestCheckMethod _checkMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = Reflect.Func<ICheckable<int>, int, Check<int>>(Checkable.IsEqualTo);

			_argument = 1;

			_checkMethod = new TestCheckMethod { Method = _method, Argument = _argument };
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
		public void HasOneArgument()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(1));
		}

		[Then]
		public void ArgumentIsOriginal()
		{
			Assert.That(_rule.CheckArguments.Single(), Is.EqualTo(_argument));
		}

		private sealed class TestCheckMethod : CheckMethod
		{
			internal MethodInfo Method { get; set; }

			internal int Argument { get; set; }

			protected override bool SupportsTargetType(Type targetType)
			{
				return true;
			}

			protected override MethodInfo GetMethod(Type targetType, Type checkType)
			{
				return Method;
			}

			protected override IEnumerable<object> GetCheckArguments(Type targetType)
			{
				yield return Argument;
			}
		}
	}
}