using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Grasp.Checks.Rules;
using Xunit;

namespace Grasp.Knowledge.Structure
{
	public class RangeQuestions
	{
		[Fact] public void Create()
		{
			var minimum = new RangeBoundaryQuestion(new Identifier("Minimum"), new ValueQuestion<int>());
			var maximum = new RangeBoundaryQuestion(new Identifier("Maximum"), new ValueQuestion<int>());
			var validVariableName = new Identifier("Valid");
			var name = new FullName("Q");

			var question = new RangeQuestion(minimum, maximum, validVariableName, name);

			question.Minimum.Should().Be(minimum);
			question.Maximum.Should().Be(maximum);
			question.ValidVariableName.Should().Be(validVariableName);
			question.Name.Should<FullName>().Be(name);
		}

		[Fact] public void GetSchema()
		{
			var question = new RangeQuestion(
				new RangeBoundaryQuestion(new Identifier("Minimum"), new ValueQuestion<int>()),
				new RangeBoundaryQuestion(new Identifier("Maximum"), new ValueQuestion<int>()),
				new Identifier("Valid"));

			var schema = question.GetSchema("SomeRange");

			schema.ShouldHaveVariables("SomeRange.Minimum", "SomeRange.Maximum");

			schema.Calculations.Should().HaveCount(1);

			var calculation = schema.Calculations.Single();

			calculation.ShouldCalculate("SomeRange.__validation.Valid", typeof(bool));

			calculation.Expression.Should().BeAssignableTo<BinaryExpression>();

			var lessThanOrEqual = (BinaryExpression) calculation.Expression;

			lessThanOrEqual.NodeType.Should().Be(ExpressionType.LessThanOrEqual);
			lessThanOrEqual.Left.Should().BeAssignableTo<VariableExpression>();
			lessThanOrEqual.Right.Should().BeAssignableTo<VariableExpression>();

			((VariableExpression) lessThanOrEqual.Left).Variable.ShouldHaveName("SomeRange.Minimum");
			((VariableExpression) lessThanOrEqual.Right).Variable.ShouldHaveName("SomeRange.Maximum");
		}

		[Fact] public void GetSchemaWithValidators()
		{
			var question = new RangeQuestion(
				new RangeBoundaryQuestion(
					new Identifier("Minimum"),
					new ValueQuestion<int>(Params.Of(new Validator("SomeMinimumRule", Expression.Constant(true))))),
				new RangeBoundaryQuestion(
					new Identifier("Maximum"),
					new ValueQuestion<int>(Params.Of(new Validator("SomeMaximumRule", Expression.Constant(true))))),
				new Identifier("Valid"));

			var schema = question.GetSchema("SomeRange");

			schema.ShouldHaveVariables("SomeRange.Minimum", "SomeRange.Maximum");

			schema.ShouldCalculate(
				"SomeRange.Minimum.__validation.SomeMinimumRule",
				"SomeRange.Maximum.__validation.SomeMaximumRule",
				"SomeRange.__validation.Valid");
		}
	}
}