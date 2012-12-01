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
			var variableType = typeof(int);
			
			var question = new ValueQuestion(variableType);

			question.Name.Should<FullName>().Be(FullName.Anonymous);
			question.VariableType.Should().Be(variableType);
			question.ValidationRules.Should().BeEmpty();
		}

		[Fact] public void CreateWithName()
		{
			var name = new FullName("Value");

			var question = new ValueQuestion<int>(name: name);

			question.Name.Should<FullName>().Be(name);
		}

		[Fact] public void CreateWithValidationRules()
		{
			var rule1 = new ValidationRule("SomeRule1", Rule.True);
			var rule2 = new ValidationRule("SomeRule2", Rule.True);

			var question = new ValueQuestion<int>(Params.Of(rule1, rule2));

			question.ValidationRules.Should().ContainInOrder(rule1, rule2);
		}

		[Fact] public void GetSchema()
		{
			var variableType = typeof(int);
			var question = new ValueQuestion(variableType);

			var schema = question.GetSchema("SomeValue");

			schema.Variables.Should().HaveCount(1);

			var variable = schema.Variables.Single();

			variable.ShouldHaveName("SomeValue");

			variable.Type.Should().Be(variableType);
		}

		[Fact] public void GetSchemaWithValidationRule()
		{
			var question = new ValueQuestion<int>(Params.Of(new ValidationRule("SomeRule", Rule.True)));

			var schema = question.GetSchema("SomeValue");

			schema.ShouldHaveVariables("SomeValue");

			schema.Calculations.Should().HaveCount(1);

			schema.Calculations.Single().ShouldCalculate("SomeValue.__validation.SomeRule", typeof(bool));
		}
	}
}