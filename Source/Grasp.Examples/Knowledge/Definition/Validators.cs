using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Grasp.Checks.Rules;
using Xunit;

namespace Grasp.Knowledge.Definition
{
	public class Validators
	{
		[Fact] public void Create()
		{
			var outputVariableIdentifier = new Identifier("O");
			var expression = Expression.Constant(true);

			var validator = new Validator(outputVariableIdentifier, expression);

			validator.OutputVariableIdentifier.Should().Be(outputVariableIdentifier);
			validator.Expression.Should().Be(expression);
		}

		[Fact] public void GetRule()
		{
			var variable = new Variable<int>("SomeValue");
			var outputVariableIdentifier = new Identifier("SomeRule");
			var expression = Expression.Constant(true);
			var validator = new Validator(outputVariableIdentifier, expression);

			var rule = validator.GetRule(variable);

			rule.Should().BeAssignableTo<ValueRule>();

			var valueRule = (ValueRule) rule;

			valueRule.Variable.Should().Be(variable);
			valueRule.OutputVariableIdentifier.Should().Be(outputVariableIdentifier);
			valueRule.Expression.Should().Be(expression);
		}
	}
}