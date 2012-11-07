using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Grasp.Analysis.Compilation
{
	public class Executables
	{
		[Fact] public void Create()
		{
			var schema = new GraspSchema();
			var calculator = A.Fake<ICalculator>();

			var executable = new GraspExecutable(schema, calculator);

			executable.Schema.Should().Be(schema);
			executable.Calculator.Should().Be(calculator);
		}

		[Fact]
		public void GetRuntime()
		{
			var variable = new Variable<int>("X");
			var schema = new GraspSchema(Params.Of(variable));
			var initialState = A.Fake<IRuntimeSnapshot>();
			var initialValue = 1;
			var executable = schema.Compile();

			A.CallTo(() => initialState.GetValue(variable)).Returns(initialValue);

			var runtime = executable.GetRuntime(initialState);

			runtime.GetVariableValue(variable).Should().Be(initialValue);
		}
	}
}