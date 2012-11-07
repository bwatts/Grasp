using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Grasp.Analysis
{
	public class Runtimes
	{
		[Fact] public void Create()
		{
			var schema = new GraspSchema();
			var calculator = A.Fake<ICalculator>();

			var runtime = new GraspRuntime(schema, calculator);

			runtime.Schema.Should().Be(schema);
			runtime.Calculator.Should().Be(calculator);
		}

		[Fact] public void GetBoundVariableValue()
		{
			var variable = new Variable<int>("X");
			var schema = new GraspSchema(Params.Of(variable));
			var value = 1;
			var runtime = new GraspRuntime(schema, A.Fake<ICalculator>(), new VariableBinding(variable, value));

			var runtimeValue = runtime.GetVariableValue(variable);

			runtimeValue.Should().Be(value);
		}

		[Fact] public void GetUnboundVariableValue()
		{
			var variable = new Variable<int>("X");
			var schema = new GraspSchema(Params.Of(variable));
			var runtime = new GraspRuntime(schema, A.Fake<ICalculator>());

			Assert.Throws<UnboundVariableException>(() => runtime.GetVariableValue(variable));
		}

		[Fact] public void SetBoundVariableValue()
		{
			var variable = new Variable<int>("X");
			var schema = new GraspSchema(Params.Of(variable));
			var binding = new VariableBinding(variable, 1);
			var runtime = new GraspRuntime(schema, A.Fake<ICalculator>(), binding);
			var newValue = 2;

			runtime.SetVariableValue(variable, newValue);

			binding.Value.Should().Be(newValue);
		}

		[Fact] public void SetUnboundVariableValue()
		{
			var variable = new Variable<int>("X");
			var schema = new GraspSchema(Params.Of(variable));
			var runtime = new GraspRuntime(schema, A.Fake<ICalculator>());
			var newValue = 1;

			runtime.SetVariableValue(variable, newValue);

			runtime.GetVariableValue(variable).Should().Be(newValue);
		}
	}
}