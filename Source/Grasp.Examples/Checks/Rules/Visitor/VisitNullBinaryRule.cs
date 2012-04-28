using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules.Visitor
{
	public class VisitNullBinaryRule : Behavior
	{
		TestRuleVisitor _visitor;
		Rule _rule;

		protected override void Given()
		{
			_visitor = new TestRuleVisitor();
		}

		protected override void When()
		{
			_rule = _visitor.VisitNull();
		}

		[Then]
		public void IsNull()
		{
			Assert.That(_rule, Is.Null);
		}

		private class TestRuleVisitor : RuleVisitor
		{
			internal Rule VisitNull()
			{
				return VisitBinary(null);
			}
		}
	}
}