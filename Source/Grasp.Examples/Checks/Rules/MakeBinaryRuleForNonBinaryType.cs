using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class MakeBinaryRuleForNonBinaryType : Behavior
	{
		Exception _exception;

		protected override void Given()
		{}

		protected override void When()
		{
			try
			{
				Rule.MakeBinary(RuleType.Check, Rule.Constant(true), Rule.Constant(true));
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
	}
}