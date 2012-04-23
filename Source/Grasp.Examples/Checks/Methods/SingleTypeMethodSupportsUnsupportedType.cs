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
	public class SingleTypeMethodSupportsUnsupportedType : Behavior
	{
		TestSingleTypeCheckMethod _method;
		bool _isSupported;

		protected override void Given()
		{
			_method = new TestSingleTypeCheckMethod();
		}

		protected override void When()
		{
			_isSupported = _method.SupportsTargetType();
		}

		[Then]
		public void TypeIsNotSupported()
		{
			Assert.That(_isSupported, Is.False);
		}

		private sealed class TestSingleTypeCheckMethod : SingleTypeCheckMethod
		{
			internal bool SupportsTargetType()
			{
				return SupportsTargetType(typeof(string));
			}

			protected override Type TargetType
			{
				get { return typeof(int); }
			}

			protected override MethodInfo GetMethod(Type checkType)
			{
				throw new NotImplementedException();
			}
		}
	}
}