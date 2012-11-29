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
	public class ValueQuestions
	{
		[Fact] public void Create()
		{
			var name = new FullName("Q");
			var variableType = typeof(int);
			var variableName = new Identifier("A");
			
			var question = new ValueQuestion(name, variableType, variableName);

			question.Name.Should<FullName>().Be(name);
			question.VariableType.Should().Be(variableType);
			question.VariableName.Should().Be(variableName);
			question.ValidationRules.Should().BeEmpty();
		}

		[Fact] public void CreateWithValidationRules()
		{
			var rule1 = new ValidationRule("R1", Rule.Constant(true));
			var rule2 = new ValidationRule("R2", Rule.Constant(true));

			var question = new ValueQuestion(new FullName("Q"), typeof(int), new Identifier("A"), Params.Of(rule1, rule2));

			question.ValidationRules.Should().ContainInOrder(rule1, rule2);
		}

		[Fact] public void GetSchema()
		{
			var variableType = typeof(int);
			var question = new ValueQuestion(new FullName("Q"), variableType, new Identifier("SomeVariable"));

			var schema = question.GetSchema("Acme");

			schema.Variables.Should().HaveCount(1);

			var variable = schema.Variables.Single();

			variable.Type.Should().Be(variableType);
			variable.Name.Value.Should().Be("Acme.SomeVariable");
		}

		[Fact] public void GetSchemaWithValidationRule()
		{
			var rule = new ValidationRule("SomeRule", Rule.Constant(true));
			var question = new ValueQuestion(new FullName("Q"), typeof(int), new Identifier("SomeVariable"), Params.Of(rule));

			var schema = question.GetSchema("Acme");

			schema.Variables.Should().HaveCount(1);
			schema.Calculations.Should().HaveCount(1);

			var calculation = schema.Calculations.Single();

			calculation.OutputVariable.Type.Should().Be(typeof(bool));
			calculation.OutputVariable.Name.Value.Should().Be("Acme.SomeVariable.__validation.SomeRule");
		}
	}
}