using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge.Runtime.Compilation
{
	public class Executables
	{
		[Fact] public void Create()
		{
			var schema = Schema.Empty;
			var calculator = A.Fake<ICalculator>();

			var executable = new Executable(schema, calculator);

			Executable._schemaField.Get(executable).Should().Be(schema);
			Executable._calculatorField.Get(executable).Should().Be(calculator);
		}

		[Fact] public void Bind()
		{
			var variable = new Variable<int>("A");
			var schema = new Schema(Params.Of(variable));
			var initialState = A.Fake<ISnapshot>();
			var initialValue = 1;
			var executable = schema.Compile();

			A.CallTo(() => initialState.GetValue(variable.Name)).Returns(initialValue);

			var binding = executable.Bind(initialState);

			binding.GetVariableValue(variable.Name).Should().Be(initialValue);
		}
	}
}