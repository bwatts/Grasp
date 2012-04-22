using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Schemas
{
	public class GetConditionsWithNoSources : Behavior
	{
		CompositeConditionSource _compositeSource;
		IEnumerable<Condition> _conditions;

		protected override void Given()
		{
			_compositeSource = new CompositeConditionSource();
		}

		protected override void When()
		{
			_conditions = _compositeSource.GetConditions();
		}

		[Then]
		public void ContainsNoConditions()
		{
			Assert.That(!_conditions.Any());
		}
	}
}