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
	public class GridQuestions
	{
		[Fact] public void Create()
		{
			var name = new FullName("Q");
			var question = new SubQuestion("SomeQuestion", new ValueQuestion<int>());
			var itemVariableName = new Identifier("SomeItem");

			var gridQuestion = new GridQuestion(Params.Of(question), Params.Of(itemVariableName), name);

			gridQuestion.Name.Should<FullName>().Be(name);
			gridQuestion.Questions.Should().HaveCount(1);
			gridQuestion.Questions.Single().Should().Be(question);
			gridQuestion.ItemVariableNames.Should().HaveCount(1);
			gridQuestion.ItemVariableNames.Single().Should().Be(itemVariableName);
		}

		[Fact] public void GetSchema()
		{
			var question = new GridQuestion(
				Params.Of(new SubQuestion("SomeQuestion", new ValueQuestion<int>())),
				Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables("SomeGrid.SomeItem.SomeQuestion");

			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithMultipleQuestions()
		{
			var question = new GridQuestion(
				Params.Of(
					new SubQuestion("SomeQuestion1", new ValueQuestion<int>()),
					new SubQuestion("SomeQuestion2", new ValueQuestion<int>())),
				Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables(
				"SomeGrid.SomeItem.SomeQuestion1",
				"SomeGrid.SomeItem.SomeQuestion2");

			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithMultipleItems()
		{
			var question = new GridQuestion(
				Params.Of(new SubQuestion("SomeQuestion", new ValueQuestion<int>())),
				Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables(
				"SomeGrid.SomeItem1.SomeQuestion",
				"SomeGrid.SomeItem2.SomeQuestion");

			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithMultipleQuestionsAndItems()
		{
			var question = new GridQuestion(
				Params.Of(
					new SubQuestion("SomeQuestion1", new ValueQuestion<int>()),
					new SubQuestion("SomeQuestion2", new ValueQuestion<int>())),
				Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables(
				"SomeGrid.SomeItem1.SomeQuestion1",
				"SomeGrid.SomeItem1.SomeQuestion2",
				"SomeGrid.SomeItem2.SomeQuestion1",
				"SomeGrid.SomeItem2.SomeQuestion2");

			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithValidationRule()
		{
			var columnQuestion = new SubQuestion(
				"SomeQuestion",
				new ValueQuestion<int>(Params.Of(new ValidationRule("SomeRule", Rule.True))));

			var question = new GridQuestion(Params.Of(columnQuestion), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables("SomeGrid.SomeItem.SomeQuestion");

			schema.ShouldCalculate("SomeGrid.SomeItem.SomeQuestion.__validation.SomeRule");
		}

		[Fact] public void GetSchemaWithValidationRuleOnMultipleQuestions()
		{
			var columnQuestion1 = new SubQuestion(
				"SomeQuestion1",
				new ValueQuestion<int>(Params.Of(new ValidationRule("SomeRule1", Rule.True))));

			var columnQuestion2 = new SubQuestion(
				"SomeQuestion2",
				new ValueQuestion<int>(Params.Of(new ValidationRule("SomeRule2", Rule.True))));

			var question = new GridQuestion(Params.Of(columnQuestion1, columnQuestion2), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables(
				"SomeGrid.SomeItem.SomeQuestion1",
				"SomeGrid.SomeItem.SomeQuestion2");
			
			schema.ShouldCalculate(
				"SomeGrid.SomeItem.SomeQuestion1.__validation.SomeRule1",
				"SomeGrid.SomeItem.SomeQuestion2.__validation.SomeRule2");
		}

		[Fact] public void GetSchemaWithValidationRuleOnMultipleQuestionsWithMultipleItems()
		{
			var columnQuestion1 = new SubQuestion(
				"SomeQuestion1",
				new ValueQuestion<int>(Params.Of(new ValidationRule("SomeRule1", Rule.True))));

			var columnQuestion2 = new SubQuestion(
				"SomeQuestion2",
				new ValueQuestion<int>(Params.Of(new ValidationRule("SomeRule2", Rule.True))));

			var question = new GridQuestion(
				Params.Of(columnQuestion1, columnQuestion2),
				Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables(
				"SomeGrid.SomeItem1.SomeQuestion1",
				"SomeGrid.SomeItem1.SomeQuestion2",
				"SomeGrid.SomeItem2.SomeQuestion1",
				"SomeGrid.SomeItem2.SomeQuestion2");

			schema.ShouldCalculate(
				"SomeGrid.SomeItem1.SomeQuestion1.__validation.SomeRule1",
				"SomeGrid.SomeItem1.SomeQuestion2.__validation.SomeRule2",
				"SomeGrid.SomeItem2.SomeQuestion1.__validation.SomeRule1",
				"SomeGrid.SomeItem2.SomeQuestion2.__validation.SomeRule2");
		}

		[Fact] public void GetSchemaWithMultipleValidationRules()
		{
			var columnQuestion = new SubQuestion(
				"SomeQuestion",
				new ValueQuestion<int>(Params.Of(
					new ValidationRule("SomeRule1", Rule.True),
					new ValidationRule("SomeRule2", Rule.True))));

			var question = new GridQuestion(Params.Of(columnQuestion), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables("SomeGrid.SomeItem.SomeQuestion");

			schema.ShouldCalculate(
				"SomeGrid.SomeItem.SomeQuestion.__validation.SomeRule1",
				"SomeGrid.SomeItem.SomeQuestion.__validation.SomeRule2");
		}

		[Fact] public void GetSchemaWithMultipleValidationRulesAndQuestions()
		{
			var columnQuestion1 = new SubQuestion(
				"SomeQuestion1",
				new ValueQuestion<int>(Params.Of(
					new ValidationRule("SomeRule1", Rule.True),
					new ValidationRule("SomeRule2", Rule.True))));

			var columnQuestion2 = new SubQuestion(
				"SomeQuestion2",
				new ValueQuestion<int>(Params.Of(
					new ValidationRule("SomeRule3", Rule.True),
					new ValidationRule("SomeRule4", Rule.True))));

			var question = new GridQuestion(Params.Of(columnQuestion1, columnQuestion2), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables(
				"SomeGrid.SomeItem.SomeQuestion1",
				"SomeGrid.SomeItem.SomeQuestion2");

			schema.ShouldCalculate(
				"SomeGrid.SomeItem.SomeQuestion1.__validation.SomeRule1",
				"SomeGrid.SomeItem.SomeQuestion1.__validation.SomeRule2",
				"SomeGrid.SomeItem.SomeQuestion2.__validation.SomeRule3",
				"SomeGrid.SomeItem.SomeQuestion2.__validation.SomeRule4");
		}

		[Fact] public void GetSchemaWithMultipleValidationRulesAndQuestionsAndItems()
		{
			var columnQuestion1 = new SubQuestion(
				"SomeQuestion1",
				new ValueQuestion<int>(Params.Of(
					new ValidationRule("SomeRule1", Rule.True),
					new ValidationRule("SomeRule2", Rule.True))));

			var columnQuestion2 = new SubQuestion(
				"SomeQuestion2",
				new ValueQuestion<int>(Params.Of(
					new ValidationRule("SomeRule3", Rule.True),
					new ValidationRule("SomeRule4", Rule.True))));

			var question = new GridQuestion(
				Params.Of(columnQuestion1, columnQuestion2),
				Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("SomeGrid");

			schema.ShouldHaveVariables(
				"SomeGrid.SomeItem1.SomeQuestion1",
				"SomeGrid.SomeItem1.SomeQuestion2",
				"SomeGrid.SomeItem2.SomeQuestion1",
				"SomeGrid.SomeItem2.SomeQuestion2");

			schema.ShouldCalculate(
				"SomeGrid.SomeItem1.SomeQuestion1.__validation.SomeRule1",
				"SomeGrid.SomeItem1.SomeQuestion1.__validation.SomeRule2",
				"SomeGrid.SomeItem1.SomeQuestion2.__validation.SomeRule3",
				"SomeGrid.SomeItem1.SomeQuestion2.__validation.SomeRule4",
				"SomeGrid.SomeItem2.SomeQuestion1.__validation.SomeRule1",
				"SomeGrid.SomeItem2.SomeQuestion1.__validation.SomeRule2",
				"SomeGrid.SomeItem2.SomeQuestion2.__validation.SomeRule3",
				"SomeGrid.SomeItem2.SomeQuestion2.__validation.SomeRule4");
		}
	}
}