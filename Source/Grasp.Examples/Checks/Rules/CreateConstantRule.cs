using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreateConstantRule : Behavior
	{
		bool _passes;
		ConstantRule _constantRule;

		protected override void Given()
		{
			_passes = true;
		}

		protected override void When()
		{
			_constantRule = Rule.Constant(_passes);
		}

		[Then]
		public void HasConstantType()
		{
			Assert.That(_constantRule.Type, Is.EqualTo(RuleType.Constant));
		}

		[Then]
		public void HasOriginalValue()
		{
			Assert.That(_constantRule.Passes, Is.EqualTo(_passes));
		}
	}
}