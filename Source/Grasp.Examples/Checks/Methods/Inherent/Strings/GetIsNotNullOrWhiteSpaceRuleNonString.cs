using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Strings
{
	public class GetIsNotNullOrWhiteSpaceRuleNonString : Behavior
	{
		IsNotNullOrWhiteSpaceMethod _method;
		GraspException _exception;

		protected override void Given()
		{
			_method = new IsNotNullOrWhiteSpaceMethod();
		}

		protected override void When()
		{
			try
			{
				_method.GetRule(typeof(int));
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
	}
}