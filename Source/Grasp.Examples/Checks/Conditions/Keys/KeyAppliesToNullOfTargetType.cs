using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class KeyAppliesToNullOfTargetType : Behavior
	{
		ConditionKey _key;
		bool _applies;

		protected override void Given()
		{
			_key = new ConditionKey(typeof(string));
		}

		protected override void When()
		{
			_applies = _key.AppliesTo(null as string);
		}

		[Then]
		public void DoesNotApply()
		{
			Assert.That(_applies, Is.False);
		}
	}
}