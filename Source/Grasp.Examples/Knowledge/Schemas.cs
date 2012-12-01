using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Grasp.Knowledge.Runtime.Compilation;
using Xunit;

namespace Grasp.Knowledge
{
	public class Schemas
	{
		[Fact] public void Create()
		{
			var variables = Params.Of(new Variable<int>("A"), new Variable<int>("B"));
			var calculations = Params.Of(
				new Calculation(new Variable<int>("X"), Expression.Constant(0)),
				new Calculation(new Variable<int>("Y"), Expression.Constant(1)));

			var schema = new Schema(variables, calculations);

			schema.Variables.SequenceEqual(variables).Should().BeTrue();
			schema.Calculations.SequenceEqual(calculations).Should().BeTrue();
		}

		[Fact] public void Compile()
		{
			var executable = Schema.Empty.Compile();

			Executable._schemaField.Get(executable).Should().Be(Schema.Empty);
		}
	}
}