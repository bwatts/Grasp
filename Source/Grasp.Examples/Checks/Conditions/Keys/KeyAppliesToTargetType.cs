using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Keys
{
	public class KeyAppliesToTargetType : Behavior
	{
		Type _targetType;
		ConditionKey _key;
		bool _applies;

		protected override void Given()
		{
			_targetType = typeof(int);

			_key = new ConditionKey(_targetType);
		}

		protected override void When()
		{
			_applies = _key.AppliesTo(_targetType);
		}

		[Then]
		public void Applies()
		{
			Assert.That(_applies);
		}
	}
}