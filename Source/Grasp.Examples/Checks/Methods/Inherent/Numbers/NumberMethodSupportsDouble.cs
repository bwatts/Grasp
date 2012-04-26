using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Numbers
{
	public class NumberMethodSupportsDouble : Behavior
	{
		TestNumberCheckMethod _method;
		bool _isSupported;

		protected override void Given()
		{
			_method = new TestNumberCheckMethod();
		}

		protected override void When()
		{
			_isSupported = _method.SupportsType();
		}

		[Then]
		public void TypeIsSupported()
		{
			Assert.That(_isSupported);
		}

		private sealed class TestNumberCheckMethod : GraspNumberCheckMethod
		{
			internal bool SupportsType()
			{
				return SupportsTargetType(typeof(double));
			}

			protected override string MethodName
			{
				get { throw new NotImplementedException(); }
			}
		}
	}
}