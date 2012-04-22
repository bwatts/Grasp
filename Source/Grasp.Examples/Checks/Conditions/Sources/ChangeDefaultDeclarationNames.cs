using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using Grasp.Checks.Rules;
using NUnit.Framework;

namespace Grasp.Checks.Conditions.Sources
{
	public class ChangeDefaultDeclarationNames : Behavior
	{
		Rule _rule1;
		Rule _rule2;
		Type _targetType;
		string _newDefaultName;
		string _nonDefaultName;
		IConditionDeclaration _declaration;
		Condition _defaultCondition;
		Condition _nonDefaultCondition;

		protected override void Given()
		{
			_rule1 = Rule.Constant(true);
			_rule2 = Rule.Constant(false);

			_newDefaultName = "NewDefault";
			_nonDefaultName = "NonDefault";

			_declaration = A.Fake<IConditionDeclaration>();

			_targetType = typeof(int);

			A.CallTo(() => _declaration.GetConditions(_targetType)).Returns(new[]
			{
				new Condition(_rule1, _targetType),
				new Condition(_rule2, _targetType, _nonDefaultName)
			});
		}

		protected override void When()
		{
			var declarationsWithDefaultName = new[] { _declaration }.WithDefaultName(_newDefaultName).Single();

			var conditions = declarationsWithDefaultName.GetConditions(_targetType).ToList();

			_defaultCondition = conditions[0];
			_nonDefaultCondition = conditions[1];
		}

		[Then]
		public void DefaultNamesChange()
		{
			Assert.That(_defaultCondition.Key.NameEquals(_newDefaultName));
		}

		[Then]
		public void DefaultTypesDoNotChange()
		{
			Assert.That(_defaultCondition.Key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void DefaultRulesDoNotChange()
		{
			Assert.That(_defaultCondition.Rule, Is.EqualTo(_rule1));
		}

		[Then]
		public void NonDefaultNamesDoNotChange()
		{
			Assert.That(_nonDefaultCondition.Key.NameEquals(_nonDefaultName));
		}

		[Then]
		public void NonDefaultTypesDoNotChange()
		{
			Assert.That(_nonDefaultCondition.Key.TargetType, Is.EqualTo(_targetType));
		}

		[Then]
		public void NonDefaultRulesDoNotChange()
		{
			Assert.That(_nonDefaultCondition.Rule, Is.EqualTo(_rule2));
		}
	}
}