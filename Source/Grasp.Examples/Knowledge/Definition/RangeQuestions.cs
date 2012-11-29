using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Grasp.Checks.Rules;
using Xunit;

namespace Grasp.Knowledge.Definition
{
	public class RangeQuestions
	{
		[Fact] public void Create()
		{
			var name = new FullName("QRange");
			var minimum = new ValueQuestion("QMin", typeof(int), new Identifier("Minimum"));
			var maximum = new ValueQuestion("QMax", typeof(int), new Identifier("Maximum"));
			var validVariableName = new Identifier("Valid");

			var question = new RangeQuestion(name, minimum, maximum, validVariableName);

			question.Name.Should<FullName>().Be(name);
			question.Minimum.Should().Be(minimum);
			question.Maximum.Should().Be(maximum);
			question.ValidVariableName.Should().Be(validVariableName);
		}

		[Fact] public void GetSchema()
		{
			var question = new RangeQuestion(
				"QRange",
				new ValueQuestion("QMin", typeof(int), new Identifier("Minimum")),
				new ValueQuestion("QMax", typeof(int), new Identifier("Maximum")),
				new Identifier("Valid"));

			var schema = question.GetSchema("Acme.SomeRange");

			schema.Variables.Should().HaveCount(2);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeRange.Minimum",
				"Acme.SomeRange.Maximum"
			});

			schema.Calculations.Should().HaveCount(1);

			var calculation = schema.Calculations.Single();

			calculation.OutputVariable.Name.Value.Should().Be("Acme.SomeRange.__validation.Valid");
			calculation.Expression.Should().BeAssignableTo<BinaryExpression>();

			var lessThanOrEqual = (BinaryExpression) calculation.Expression;

			lessThanOrEqual.NodeType.Should().Be(ExpressionType.LessThanOrEqual);
			lessThanOrEqual.Left.Should().BeAssignableTo<VariableExpression>();
			lessThanOrEqual.Right.Should().BeAssignableTo<VariableExpression>();

			((VariableExpression) lessThanOrEqual.Left).Variable.Name.Value.Should().Be("Acme.SomeRange.Minimum");
			((VariableExpression) lessThanOrEqual.Right).Variable.Name.Value.Should().Be("Acme.SomeRange.Maximum");
		}

		[Fact] public void GetSchemaWithValidationRules()
		{
			var question = new RangeQuestion(
				"QRange",
				new ValueQuestion("QMin", typeof(int), new Identifier("Minimum"), Params.Of(new ValidationRule("SomeRule1", Rule.Constant(true)))),
				new ValueQuestion("QMax", typeof(int), new Identifier("Maximum"), Params.Of(new ValidationRule("SomeRule2", Rule.Constant(true)))),
				new Identifier("Valid"));

			var schema = question.GetSchema("Acme.SomeRange");

			schema.Variables.Should().HaveCount(2);
			schema.Calculations.Should().HaveCount(3);

			schema.Calculations.Select(calculation => calculation.OutputVariable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeRange.Minimum.__validation.SomeRule1",
				"Acme.SomeRange.Maximum.__validation.SomeRule2",
				"Acme.SomeRange.__validation.Valid",
			});
		}
	}
}