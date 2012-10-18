using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Grasp.Analysis.Compilation;
using Xbehave;

namespace Grasp.Analysis
{
	public class Schemas
	{
		[Scenario]
		public void Create(IEnumerable<Variable> variables, IEnumerable<Calculation> calculations, GraspSchema schema)
		{
			"Given a set of variables".Given(() => variables = Params.Of(new Variable<int>("X"), new Variable<int>("Y")));
			"And a set of calculations".And(() => calculations = Params.Of(
				new Calculation(new Variable<int>("A"), Expression.Constant(0)),
				new Calculation(new Variable<int>("B"), Expression.Constant(1))));

			"When creating a schema".When(() => schema = new GraspSchema(variables, calculations));

			"It has the specified variables in order".Then(() => schema.Variables.SequenceEqual(variables).Should().BeTrue());
			"It has the specified calculations in order".Then(() => schema.Calculations.SequenceEqual(calculations).Should().BeTrue());
		}

		[Scenario]
		public void Compile(GraspSchema schema, GraspExecutable executable)
		{
			"Given a schema".And(() => schema = new GraspSchema());

			"When compiling the schema".When(() => executable = schema.Compile());

			"The executable has the specified schema".Then(() => executable.Schema.Should().Be(schema));
		}
	}
}