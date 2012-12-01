using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge.Definition
{
	public class MultipleChoiceQuestions
	{
		[Fact] public void Create()
		{
			var choice1 = new Choice("Choice 1");
			var choice2 = new Choice("Choice 2");
			var name = new FullName("Q");

			var question = new MultipleChoiceQuestion(Params.Of(choice1, choice2), name);

			question.Name.Should<FullName>().Be(name);
			question.Choices.Should().HaveCount(2);
			question.Choices.Should().ContainInOrder(choice1, choice2);
		}

		[Fact] public void GetSchema()
		{
			var question = new MultipleChoiceQuestion();

			var schema = question.GetSchema("SomeMultipleChoice");

			schema.ShouldHaveVariables("SomeMultipleChoice");
		}

		[Fact] public void GetSchemaWithSubQuestion()
		{
			var choice = new Choice("Some choice", new SubQuestion("Other", new ValueQuestion<string>()));

			var question = new MultipleChoiceQuestion(Params.Of(choice));

			var schema = question.GetSchema("SomeMultipleChoice");

			schema.ShouldHaveVariables("SomeMultipleChoice", "SomeMultipleChoice.Other");
		}
	}
}