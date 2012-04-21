using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreatePropertyRule : Behavior
	{
		PropertyInfo _property;
		Rule _rule;
		MemberRule _propertyRule;

		protected override void Given()
		{
			_property = Reflect.Property<TestClass, int>(x => x.TestProperty);

			_rule = Rule.Constant(true);
		}

		protected override void When()
		{
			_propertyRule = Rule.Property(_property, _rule);
		}

		[Then]
		public void HasPropertyType()
		{
			Assert.That(_propertyRule.Type, Is.EqualTo(RuleType.Property));
		}

		[Then]
		public void HasOriginalProperty()
		{
			Assert.That(_propertyRule.Member, Is.EqualTo(_property));
		}

		[Then]
		public void HasOriginalRule()
		{
			Assert.That(_propertyRule.Rule, Is.EqualTo(_rule));
		}

		[Then]
		public void MemberTypeIsPropertyType()
		{
			Assert.That(_propertyRule.MemberType, Is.EqualTo(_property.PropertyType));
		}

		private sealed class TestClass
		{
			public int TestProperty { get; set; }
		}
	}
}