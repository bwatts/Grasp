using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class MakeMemberRuleForNonMemberType : Behavior
	{
		ConstructorInfo _constructor;
		Exception _exception;

		protected override void Given()
		{
			_constructor = Reflect.Constructor(() => new TestClass());
		}

		protected override void When()
		{
			try
			{
				Rule.MakeMember(_constructor, Rule.Constant(true));
			}
			catch(Exception ex)
			{
				_exception = ex;
			}
		}

		[Then]
		public void Throws()
		{
			Assert.That(_exception, Is.Not.Null);
		}

		private sealed class TestClass
		{
			public TestClass()
			{}
		}
	}
}