using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class GetDefaultName : Behavior
	{
		bool _isEmpty;

		protected override void Given()
		{}

		protected override void When()
		{
			_isEmpty = ConditionKey.DefaultName == "";
		}

		[Then]
		public void IsEmpty()
		{
			Assert.That(_isEmpty);
		}
	}
}