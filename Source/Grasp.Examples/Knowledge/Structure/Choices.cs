using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge.Structure
{
	public class Choices
	{
		[Fact] public void Create()
		{
			var value = "Some choice";

			var question = new Choice(value);

			question.Value.Should().Be(value);
			question.SubQuestion.Should().BeNull();
			question.HasSubQuestion.Should().BeFalse();
		}

		[Fact] public void CreateWithSubQuestion()
		{
			var value = "Some choice";
			var subQuestion = new SubQuestion("Other", new ValueQuestion<string>());

			var question = new Choice(value, subQuestion);

			question.Value.Should().Be(value);
			question.SubQuestion.Should().Be(subQuestion);
			question.HasSubQuestion.Should().BeTrue();
		}

		[Fact] public void GetSchema()
		{
			var choice = new Choice("Some choice");

			var schema = choice.GetSchema(new Namespace("SomeChoice"));

			schema.Variables.Should().BeEmpty();
			schema.Calculations.Should().BeEmpty();
		}

		[Fact] public void GetSchemaWithSubQuestion()
		{
			var choice = new Choice("Some choice", new SubQuestion("Other", new ValueQuestion<string>()));

			var schema = choice.GetSchema(new Namespace("SomeChoice"));

			schema.ShouldHaveVariables("SomeChoice.Other");
			schema.Calculations.Should().BeEmpty();
		}
	}
}