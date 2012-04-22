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
	public class GetSpecification : Behavior
	{
		TestTarget _target;
		string _conditionName;
		Rule _rule;
		ConditionSchema _schema;
		Specification<TestTarget> _specification;

		protected override void Given()
		{
			_target = new TestTarget();
			_conditionName = "Test";

			_rule = Rule.Constant(true);

			var key = new ConditionKey(_target.GetType(), _conditionName);

			var conditionRepository = A.Fake<IConditionRepository>();
			
			A.CallTo(() => conditionRepository.GetCondition(A<ConditionKey>.That.IsEqualTo(key))).Returns(new Condition(_rule, key));

			_schema = new ConditionSchema(conditionRepository);
		}

		protected override void When()
		{
			_specification = _schema.GetSpecification(_target, _conditionName);
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
		public void HasConditionRule()
		{
			Assert.That(_specification.Rule, Is.EqualTo(_rule));
		}

		private class TestTarget
		{}
	}
}