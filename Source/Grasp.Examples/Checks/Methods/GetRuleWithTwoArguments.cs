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
	public class GetRuleWithTwoArguments : Behavior
	{
		MethodInfo _method;
		int _argument1;
		int _argument2;
		TestCheckMethod _checkMethod;
		CheckRule _rule;

		protected override void Given()
		{
			_method = Reflect.Func<ICheckable<int>, int, int, Check<int>>(Checkable.IsBetween);

			_argument1 = 1;
			_argument2 = 2;

			_checkMethod = new TestCheckMethod { Method = _method, Argument1 = _argument1, Argument2 = _argument2 };
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
		public void HasTwoArguments()
		{
			Assert.That(_rule.CheckArguments.Count(), Is.EqualTo(2));
		}

		[Then]
		public void FirstArgumentIsOriginal()
		{
			Assert.That(_rule.CheckArguments[0], Is.EqualTo(_argument1));
		}

		[Then]
		public void SecondArgumentIsOriginal()
		{
			Assert.That(_rule.CheckArguments[1], Is.EqualTo(_argument2));
		}

		private sealed class TestCheckMethod : CheckMethod
		{
			internal MethodInfo Method { get; set; }

			internal int Argument1 { get; set; }

			internal int Argument2 { get; set; }

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
				yield return Argument1;
				yield return Argument2;
			}
		}
	}
}