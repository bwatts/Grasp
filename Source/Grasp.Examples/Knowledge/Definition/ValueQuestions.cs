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
			question.Validators.Should().BeEmpty();
		}

		[Fact] public void CreateWithName()
		{
			var name = new FullName("Value");

			var question = new ValueQuestion<int>(name: name);

			question.Name.Should<FullName>().Be(name);
		}

		[Fact] public void CreateWithValidators()
		{
			var validator1 = new Validator("SomeRule1", Expression.Constant(true));
			var validator2 = new Validator("SomeRule2", Expression.Constant(true));

			var question = new ValueQuestion<int>(Params.Of(validator1, validator2));

			question.Validators.Should().ContainInOrder(validator1, validator2);
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

		[Fact] public void GetSchemaWithValidator()
		{
			var question = new ValueQuestion<int>(Params.Of(new Validator("SomeRule", Expression.Constant(true))));

			var schema = question.GetSchema("SomeValue");

			schema.ShouldHaveVariables("SomeValue");

			schema.Calculations.Should().HaveCount(1);
			schema.Calculations.Single().ShouldCalculate("SomeValue.__validation.SomeRule", typeof(bool));
		}
	}
}