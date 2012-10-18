using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis.Compilation
{
	public class Executables
	{
		[Scenario]
		public void Create(GraspSchema schema, ICalculator calculator, GraspExecutable executable)
		{
			"Given an empty schema".Given(() => schema = new GraspSchema());
			"And a calculator".And(() => calculator = A.Fake<ICalculator>());

			"When compiling an executable from the runtime".When(() => executable = new GraspExecutable(schema, calculator));

			"It has the specified schema".Then(() => executable.Schema.Should().Be(schema));
			"It has the specified calculator".Then(() => executable.Calculator.Should().Be(calculator));
		}

		[Scenario]
		public void GetRuntime(Variable variable, GraspSchema schema, IRuntimeSnapshot initialState, int initialValue, GraspExecutable executable, GraspRuntime runtime)
		{
			"Given a variabe".Given(() => variable = new Variable<int>("X"));
			"And a schema with the variable".Given(() => schema = new GraspSchema(Params.Of(variable)));
			"And an initial state with a value for the variable".And(() =>
			{
				initialState = A.Fake<IRuntimeSnapshot>();

				A.CallTo(() => initialState.GetValue(variable)).Returns(initialValue);
			});
			"And a compiled executable from the schema".And(() => executable = schema.Compile());

			"When getting a runtime for the specified executable and initial state".When(() => runtime = executable.GetRuntime(initialState));

			"It binds the variable to the initial value".Then(() => runtime.GetVariableValue(variable).Should().Be(initialValue));
		}
	}
}