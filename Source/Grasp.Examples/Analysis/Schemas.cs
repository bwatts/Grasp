using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Grasp.Analysis.Compilation;
using Xunit;

namespace Grasp.Analysis
{
	public class Schemas
	{
		[Fact] public void Create()
		{
			var variables = Params.Of(new Variable<int>("X"), new Variable<int>("Y"));
			var calculations = Params.Of(
				new Calculation(new Variable<int>("A"), Expression.Constant(0)),
				new Calculation(new Variable<int>("B"), Expression.Constant(1)));

			var schema = new GraspSchema(variables, calculations);

			schema.Variables.SequenceEqual(variables).Should().BeTrue();
			schema.Calculations.SequenceEqual(calculations).Should().BeTrue();
		}

		[Fact] public void Compile()
		{
			var schema = new GraspSchema();

			var executable = schema.Compile();

			executable.Schema.Should().Be(schema);
		}
	}
}