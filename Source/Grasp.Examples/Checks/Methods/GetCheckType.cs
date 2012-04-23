using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Methods
{
	public class GetCheckType : Behavior
	{
		Type _checkType;

		protected override void Given()
		{}

		protected override void When()
		{
			_checkType = TestCheckMethod.GetCheckType();
		}

		[Then]
		public void IsOriginalTargetType()
		{
			Assert.That(_checkType, Is.EqualTo(typeof(ICheckable<int>)));
		}

		private sealed class TestCheckMethod : CheckMethod
		{
			internal static Type GetCheckType()
			{
				return GetCheckType(typeof(int));
			}

			protected override bool SupportsTargetType(Type targetType)
			{
				throw new NotImplementedException();
			}

			protected override MethodInfo GetMethod(Type targetType, Type checkType)
			{
				throw new NotImplementedException();
			}
		}
	}
}