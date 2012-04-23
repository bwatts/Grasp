using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Methods
{
	public class GetCheckArguments : Behavior
	{
		private TestCheckMethod _checkMethod;
		private IEnumerable<object> _arguments;

		protected override void Given()
		{
			_checkMethod = new TestCheckMethod();
		}

		protected override void When()
		{
			_arguments = _checkMethod.GetCheckArguments();
		}

		[Then]
		public void HasNoArguments()
		{
			Assert.That(_arguments.Any(), Is.False);
		}

		private sealed class TestCheckMethod : CheckMethod
		{
			internal IEnumerable<object> GetCheckArguments()
			{
				return GetCheckArguments(typeof(int));
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