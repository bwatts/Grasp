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
	public class SingleTypeMethodSupportsDerivedType : Behavior
	{
		TestSingleTypeCheckMethod _method;
		bool _isSupported;

		protected override void Given()
		{
			_method = new TestSingleTypeCheckMethod();
		}

		protected override void When()
		{
			_isSupported = _method.SupportsDerivedTargetType();
		}

		[Then]
		public void TypeIsSupported()
		{
			Assert.That(_isSupported);
		}

		private class BaseTargetType
		{}

		private class DerivedTargetType : BaseTargetType
		{}

		private sealed class TestSingleTypeCheckMethod : SingleTypeCheckMethod
		{
			internal bool SupportsDerivedTargetType()
			{
				return SupportsTargetType(typeof(DerivedTargetType));
			}

			protected override Type TargetType
			{
				get { return typeof(BaseTargetType); }
			}

			protected override MethodInfo GetMethod(Type checkType)
			{
				throw new NotImplementedException();
			}
		}
	}
}