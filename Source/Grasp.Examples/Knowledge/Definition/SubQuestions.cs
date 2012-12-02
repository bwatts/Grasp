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
	public class SubQuestions
	{
		[Fact] public void Create()
		{
			var variableName = new Identifier("SomeSubQuestion");
			var question = new ValueQuestion<int>();
			var name = new FullName("Q");

			var subQuestion = new SubQuestion(variableName, question, name);

			subQuestion.VariableName.Should().Be(variableName);
			subQuestion.Question.Should().Be(question);
			subQuestion.Name.Should<FullName>().Be(name);
		}

		[Fact] public void GetSchema()
		{
			var question = new SubQuestion(new Identifier("SomeSubQuestion"), new ValueQuestion<int>());

			var schema = question.GetSchema("SomeQuestion");

			schema.ShouldHaveVariables("SomeQuestion.SomeSubQuestion");
		}
	}
}