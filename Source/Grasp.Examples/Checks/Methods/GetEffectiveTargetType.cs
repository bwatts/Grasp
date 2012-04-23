using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Methods
{
	public class GetEffectiveTargetType : Behavior
	{
		TestCheckMethod _checkMethod;
		Type _effectiveTargetType;

		protected override void Given()
		{
			_checkMethod = new TestCheckMethod();
		}

		protected override void When()
		{
			_effectiveTargetType = _checkMethod.GetEffectiveTargetType();
		}

		[Then]
		public void IsOriginalTargetType()
		{
			Assert.That(_effectiveTargetType, Is.EqualTo(typeof(int)));
		}

		private sealed class TestCheckMethod : CheckMethod
		{
			internal Type GetEffectiveTargetType()
			{
				return GetEffectiveTargetType(typeof(int));
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