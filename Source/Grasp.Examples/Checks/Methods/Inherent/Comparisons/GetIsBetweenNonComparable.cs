using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Methods.Inherent.Comparisons
{
	public class GetIsBetweenNonComparable : Behavior
	{
		IsBetweenMethod _method;
		GraspException _exception;

		protected override void Given()
		{
			_method = new IsBetweenMethod(1, 10);
		}

		protected override void When()
		{
			try
			{
				_method.GetRule(typeof(NonComparable));
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

		private class NonComparable
		{}
	}
}