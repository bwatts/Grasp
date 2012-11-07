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
	public class Calculators
	{
		[Fact] public void CreateFunctionCalculator()
		{
			var outputVariable = new Variable<int>("X");
			Func<GraspRuntime, object> function = runtime => 1;

			var calculator = new FunctionCalculator(outputVariable, function);

			calculator.OutputVariable.Should().Be(outputVariable);
			calculator.Function.Should().Be(function);
		}

		[Fact] public void CreateCompositeCalculator()
		{
			var calculators = Params.Of(A.Fake<ICalculator>(), A.Fake<ICalculator>());

			var calculator = new CompositeCalculator(calculators);

			calculator.Calculators.SequenceEqual(calculators);
		}

		[Fact] public void ApplyFunctionCalculator()
		{
			var outputVariable = new Variable<int>("X");
			var value = 1;
			var calculator = new FunctionCalculator(outputVariable, runtime2 => value);
			var schema = new GraspSchema();
			var runtime = new GraspRuntime(schema, calculator);

			calculator.ApplyCalculation(runtime);

			runtime.GetVariableValue(outputVariable).Should().Be(value);
		}

		[Fact] public void ApplyCompositeCalculator()
		{
			var value1 = 1;
			var value2 = 2;
			var outputVariable1 = new Variable<int>("X");
			var outputVariable2 = new Variable<int>("Y");
			var calculator1 = new FunctionCalculator(outputVariable1, runtime1 => value1);
			var calculator2 = new FunctionCalculator(outputVariable2, runtime2 => value2);
			var compositeCalculator = new CompositeCalculator(calculator1, calculator2);
			var runtime = new GraspRuntime(new GraspSchema(), compositeCalculator);

			compositeCalculator.ApplyCalculation(runtime);

			runtime.GetVariableValue(outputVariable1).Should().Be(value1);
			runtime.GetVariableValue(outputVariable2).Should().Be(value2);
		}
	}
}