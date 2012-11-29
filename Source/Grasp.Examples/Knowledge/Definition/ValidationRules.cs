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
	public class ValidationRules
	{
		[Fact] public void Create()
		{
			var rule = Rule.Constant(true);
			var outputVariableIdentifier = new Identifier("O");

			var validationRule = new ValidationRule(outputVariableIdentifier, rule);

			validationRule.Rule.Should().Be(rule);
			validationRule.OutputVariableIdentifier.Should().Be(outputVariableIdentifier);
		}

		[Fact] public void GetCalculation()
		{
			var validationRule = new ValidationRule("SomeRule", Rule.Constant(true));
			var target = new Variable<int>("Grasp.SomeTarget");

			var calculation = validationRule.GetCalculation(target);

			calculation.OutputVariable.Type.Should().Be(typeof(bool));
			calculation.OutputVariable.Name.Value.Should().Be("Grasp.SomeTarget.__validation.SomeRule");
			calculation.Expression.Should().BeAssignableTo<InvocationExpression>();

			var invocation = (InvocationExpression) calculation.Expression;

			invocation.Expression.Type.Should().Be(typeof(Func<int, bool>));
			invocation.Arguments.Should().HaveCount(1);
			invocation.Arguments.Single().Should().BeAssignableTo<VariableExpression>();

			var variableArgument = (VariableExpression) invocation.Arguments.Single();

			variableArgument.Variable.Should().Be(target);
		}
	}
}