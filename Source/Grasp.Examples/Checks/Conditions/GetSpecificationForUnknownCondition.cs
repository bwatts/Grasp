using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions
{
	public class GetSpecificationForUnknownCondition : Behavior
	{
		TestTarget _target;
		ConditionSchema _schema;
		Specification<TestTarget> _specification;

		protected override void Given()
		{
			_target = new TestTarget();

			var conditionRepository = A.Fake<IConditionRepository>();
			
			A.CallTo(() => conditionRepository.GetCondition(A<ConditionKey>.Ignored)).Returns(null);

			_schema = new ConditionSchema(conditionRepository);
		}

		protected override void When()
		{
			_specification = _schema.GetSpecification(_target, ConditionKey.DefaultName);
		}

		[Then]
		public void HasTargetType()
		{
			Assert.That(_specification.TargetType, Is.EqualTo(_target.GetType()));
		}

		[Then]
		public void HasOriginalTarget()
		{
			Assert.That(_specification.Target, Is.EqualTo(_target));
		}

		[Then]
		public void HasConditionSchemaProvider()
		{
			Assert.That(_specification.Provider, Is.InstanceOf<ConditionSchemaSpecificationProvider>());
		}

		[Then]
		public void HasConstantRule()
		{
			Assert.That(_specification.Rule, Is.InstanceOf<ConstantRule>());
		}

		[Then]
		public void ConstantRulePasses()
		{
			var constantRule = (ConstantRule) _specification.Rule;

			Assert.That(constantRule.Passes);
		}

		private class TestTarget
		{}
	}
}