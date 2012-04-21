using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreateNotRule : Behavior
	{
		Rule _rule;
		NotRule _notRule;

		protected override void Given()
		{
			_rule = Rule.Constant(true);
		}

		protected override void When()
		{
			_notRule = Rule.Not(_rule);
		}

		[Then]
		public void HasNotType()
		{
			Assert.That(_notRule.Type, Is.EqualTo(RuleType.Not));
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_notRule.Rule, Is.EqualTo(_rule));
		}
	}
}