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
	public class RangeBoundaryQuestions
	{
		[Fact] public void Create()
		{
			var variableName = new Identifier("SomeRangeBoundary");
			var value = new ValueQuestion<int>();
			var name = new FullName("Q");

			var question = new RangeBoundaryQuestion(variableName, value, name);

			question.VariableName.Should().Be(variableName);
			question.Value.Should().Be(value);
			question.Name.Should<FullName>().Be(name);
		}

		[Fact] public void GetSchema()
		{
			var question = new RangeBoundaryQuestion(new Identifier("SomeRangeBoundary"), new ValueQuestion<int>());

			var schema = question.GetSchema("SomeRange");

			schema.ShouldHaveVariables("SomeRange.SomeRangeBoundary");
		}
	}
}