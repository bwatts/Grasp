using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Methods
{
	public class GetRuleForUnsupportedTargetType : Behavior
	{
		TestCheckMethod _checkMethod;
		GraspException _exception;

		protected override void Given()
		{
			_checkMethod = new TestCheckMethod();
		}

		protected override void When()
		{
			try
			{
				_checkMethod.GetRule(typeof(int));
			}
			catch(GraspException ex)
			{
				_exception = ex;
			}
		}

		[Then]
		public void Throws()
		{
			Assert.That(_exception, Is.Not.Null);
		}

		private sealed class TestCheckMethod : CheckMethod
		{
			protected override bool SupportsTargetType(Type targetType)
			{
				return false;
			}

			protected override MethodInfo GetMethod(Type targetType, Type checkType)
			{
				throw new NotImplementedException();
			}
		}
	}
}