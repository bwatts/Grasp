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
			var name = new FullName("QGrid");
			var question = new ValueQuestion("Q", typeof(int), new Identifier("SomeQuestion"));
			var itemVariableName = new Identifier("SomeItem");

			var gridQuestion = new GridQuestion(name, Params.Of(question), Params.Of(itemVariableName));

			gridQuestion.Name.Should<FullName>().Be(name);
			gridQuestion.Questions.Should().HaveCount(1);
			gridQuestion.Questions.Single().Should().Be(question);
			gridQuestion.ItemVariableNames.Should().HaveCount(1);
			gridQuestion.ItemVariableNames.Single().Should().Be(itemVariableName);
		}

		[Fact] public void GetSchema()
		{
			var question = new GridQuestion(
				"QGrid",
				Params.Of(new ValueQuestion("Q", typeof(int), new Identifier("SomeQuestion"))),
				Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(1);
			schema.Variables.Single().Name.Value.Should().Be("Acme.SomeGrid.SomeItem.SomeQuestion");
			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithMultipleQuestions()
		{
			var question = new GridQuestion(
				"QGrid",
				Params.Of(
					new ValueQuestion("Q1", typeof(int), new Identifier("SomeQuestion1")),
					new ValueQuestion("Q2", typeof(int), new Identifier("SomeQuestion2"))),
				Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(2);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem.SomeQuestion1",
				"Acme.SomeGrid.SomeItem.SomeQuestion2"
			});
			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithMultipleItems()
		{
			var question = new GridQuestion(
				"QGrid",
				Params.Of(new ValueQuestion("Q", typeof(int), new Identifier("SomeQuestion"))),
				Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(2);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem1.SomeQuestion",
				"Acme.SomeGrid.SomeItem2.SomeQuestion"
			});
			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithMultipleQuestionsAndItems()
		{
			var question = new GridQuestion(
				"QGrid",
				Params.Of(
					new ValueQuestion("Q1", typeof(int), new Identifier("SomeQuestion1")),
					new ValueQuestion("Q2", typeof(int), new Identifier("SomeQuestion2"))),
				Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(4);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem1.SomeQuestion1",
				"Acme.SomeGrid.SomeItem1.SomeQuestion2",
				"Acme.SomeGrid.SomeItem2.SomeQuestion1",
				"Acme.SomeGrid.SomeItem2.SomeQuestion2"
			});
			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithValidationRule()
		{
			var columnQuestion = new ValueQuestion(
				"Q",
				typeof(int),
				new Identifier("SomeQuestion"),
				Params.Of(new ValidationRule("SomeRule", Rule.Constant(true))));

			var question = new GridQuestion("QGrid", Params.Of(columnQuestion), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(1);
			schema.Variables.Single().Name.Value.Should().Be("Acme.SomeGrid.SomeItem.SomeQuestion");
			schema.Calculations.Should().HaveCount(1);
			schema.Calculations.Single().OutputVariable.Name.Value.Should().Be("Acme.SomeGrid.SomeItem.SomeQuestion.__validation.SomeRule");
		}

		[Fact] public void GetSchemaWithValidationRuleOnMultipleQuestions()
		{
			var columnQuestion1 = new ValueQuestion(
				"Q1",
				typeof(int),
				new Identifier("SomeQuestion1"),
				Params.Of(new ValidationRule("SomeRule1", Rule.Constant(true))));

			var columnQuestion2 = new ValueQuestion(
				"Q2",
				typeof(int),
				new Identifier("SomeQuestion2"),
				Params.Of(new ValidationRule("SomeRule2", Rule.Constant(true))));

			var question = new GridQuestion("QGrid", Params.Of(columnQuestion1, columnQuestion2), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(2);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem.SomeQuestion1",
				"Acme.SomeGrid.SomeItem.SomeQuestion2"
			});
			schema.Calculations.Should().HaveCount(2);
			schema.Calculations.Select(calculation => calculation.OutputVariable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem.SomeQuestion1.__validation.SomeRule1",
				"Acme.SomeGrid.SomeItem.SomeQuestion2.__validation.SomeRule2"
			});
		}

		[Fact] public void GetSchemaWithValidationRuleOnMultipleQuestionsWithMultipleItems()
		{
			var columnQuestion1 = new ValueQuestion(
				"Q1",
				typeof(int),
				new Identifier("SomeQuestion1"),
				Params.Of(new ValidationRule("SomeRule1", Rule.Constant(true))));

			var columnQuestion2 = new ValueQuestion(
				"Q2",
				typeof(int),
				new Identifier("SomeQuestion2"),
				Params.Of(new ValidationRule("SomeRule2", Rule.Constant(true))));

			var question = new GridQuestion(
				"QGrid",
				Params.Of(columnQuestion1, columnQuestion2),
				Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(4);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem1.SomeQuestion1",
				"Acme.SomeGrid.SomeItem1.SomeQuestion2",
				"Acme.SomeGrid.SomeItem2.SomeQuestion1",
				"Acme.SomeGrid.SomeItem2.SomeQuestion2"
			});
			schema.Calculations.Should().HaveCount(4);
			schema.Calculations.Select(calculation => calculation.OutputVariable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem1.SomeQuestion1.__validation.SomeRule1",
				"Acme.SomeGrid.SomeItem1.SomeQuestion2.__validation.SomeRule2",
				"Acme.SomeGrid.SomeItem2.SomeQuestion1.__validation.SomeRule1",
				"Acme.SomeGrid.SomeItem2.SomeQuestion2.__validation.SomeRule2"
			});
		}

		[Fact] public void GetSchemaWithMultipleValidationRules()
		{
			var columnQuestion = new ValueQuestion(
				"Q",
				typeof(int),
				new Identifier("SomeQuestion"),
				Params.Of(
					new ValidationRule("SomeRule1", Rule.Constant(true)),
					new ValidationRule("SomeRule2", Rule.Constant(true))));

			var question = new GridQuestion("QGrid", Params.Of(columnQuestion), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(1);
			schema.Variables.Single().Name.Value.Should().Be("Acme.SomeGrid.SomeItem.SomeQuestion");
			schema.Calculations.Should().HaveCount(2);
			schema.Calculations.Select(calculation => calculation.OutputVariable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem.SomeQuestion.__validation.SomeRule1",
				"Acme.SomeGrid.SomeItem.SomeQuestion.__validation.SomeRule2"
			});
		}

		[Fact] public void GetSchemaWithMultipleValidationRulesAndQuestions()
		{
			var columnQuestion1 = new ValueQuestion(
				"Q1",
				typeof(int),
				new Identifier("SomeQuestion1"),
				Params.Of(
					new ValidationRule("SomeRule1", Rule.Constant(true)),
					new ValidationRule("SomeRule2", Rule.Constant(true))));

			var columnQuestion2 = new ValueQuestion(
				"Q2",
				typeof(int),
				new Identifier("SomeQuestion2"),
				Params.Of(
					new ValidationRule("SomeRule3", Rule.Constant(true)),
					new ValidationRule("SomeRule4", Rule.Constant(true))));

			var question = new GridQuestion("QGrid", Params.Of(columnQuestion1, columnQuestion2), Params.Of(new Identifier("SomeItem")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(2);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem.SomeQuestion1",
				"Acme.SomeGrid.SomeItem.SomeQuestion2"
			});
			schema.Calculations.Should().HaveCount(4);
			schema.Calculations.Select(calculation => calculation.OutputVariable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem.SomeQuestion1.__validation.SomeRule1",
				"Acme.SomeGrid.SomeItem.SomeQuestion1.__validation.SomeRule2",
				"Acme.SomeGrid.SomeItem.SomeQuestion2.__validation.SomeRule3",
				"Acme.SomeGrid.SomeItem.SomeQuestion2.__validation.SomeRule4"
			});
		}

		[Fact] public void GetSchemaWithMultipleValidationRulesAndQuestionsAndItems()
		{
			var columnQuestion1 = new ValueQuestion(
				"Q1",
				typeof(int),
				new Identifier("SomeQuestion1"),
				Params.Of(
					new ValidationRule("SomeRule1", Rule.Constant(true)),
					new ValidationRule("SomeRule2", Rule.Constant(true))));

			var columnQuestion2 = new ValueQuestion(
				"Q2",
				typeof(int),
				new Identifier("SomeQuestion2"),
				Params.Of(
					new ValidationRule("SomeRule3", Rule.Constant(true)),
					new ValidationRule("SomeRule4", Rule.Constant(true))));

			var question = new GridQuestion("QGrid", Params.Of(columnQuestion1, columnQuestion2), Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")));

			var schema = question.GetSchema("Acme.SomeGrid");

			schema.Variables.Should().HaveCount(4);
			schema.Variables.Select(variable => variable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem1.SomeQuestion1",
				"Acme.SomeGrid.SomeItem1.SomeQuestion2",
				"Acme.SomeGrid.SomeItem2.SomeQuestion1",
				"Acme.SomeGrid.SomeItem2.SomeQuestion2"
			});
			schema.Calculations.Should().HaveCount(8);
			schema.Calculations.Select(calculation => calculation.OutputVariable.Name.Value).Should().Contain(new[]
			{
				"Acme.SomeGrid.SomeItem1.SomeQuestion1.__validation.SomeRule1",
				"Acme.SomeGrid.SomeItem1.SomeQuestion1.__validation.SomeRule2",
				"Acme.SomeGrid.SomeItem1.SomeQuestion2.__validation.SomeRule3",
				"Acme.SomeGrid.SomeItem1.SomeQuestion2.__validation.SomeRule4",
				"Acme.SomeGrid.SomeItem2.SomeQuestion1.__validation.SomeRule1",
				"Acme.SomeGrid.SomeItem2.SomeQuestion1.__validation.SomeRule2",
				"Acme.SomeGrid.SomeItem2.SomeQuestion2.__validation.SomeRule3",
				"Acme.SomeGrid.SomeItem2.SomeQuestion2.__validation.SomeRule4"
			});
		}
	}
}