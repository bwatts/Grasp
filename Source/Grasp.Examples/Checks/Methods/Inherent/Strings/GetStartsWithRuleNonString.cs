using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Strings
{
	public class GetStartsWithRuleNonString : Behavior
	{
		StartsWithMethod _method;
		GraspException _exception;

		protected override void Given()
		{
			_method = new StartsWithMethod("");
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