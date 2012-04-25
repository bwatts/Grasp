using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Comparisons
{
	public class ComparisonMethodSupportsComparable : Behavior
	{
		TestComparisonMethod _method;
		bool _isSupported;

		protected override void Given()
		{
			_method = new TestComparisonMethod();
		}

		protected override void When()
		{
			_isSupported = _method.SupportsTargetType();
		}

		[Then]
		public void TypeIsSupported()
		{
			Assert.That(_isSupported);
		}

		private sealed class TestComparisonMethod : ComparisonMethod
		{
			internal bool SupportsTargetType()
			{
				return SupportsTargetType(typeof(int));
			}

			protected override MethodInfo GetMethod(Type targetType, Type checkType)
			{
				throw new NotImplementedException();
			}
		}
	}
}