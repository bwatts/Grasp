using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis
{
	public class Calculators
	{
		[Scenario]
		public void CreateFunctionCalculator(Variable outputVariable, Func<GraspRuntime, object> function, FunctionCalculator calculator)
		{
			"Given an output variable".Given(() => outputVariable = new Variable<int>("X"));
			"And a function".And(() => function = runtime => 1);

			"When creating a calculator".When(() => calculator = new FunctionCalculator(outputVariable, function));

			"It has the specified output variable".Then(() => calculator.OutputVariable.Should().Be(outputVariable));
			"It has the specified function".Then(() => calculator.Function.Should().Be(function));
		}

		[Scenario]
		public void CreateCompositeCalculator(IEnumerable<ICalculator> calculators, CompositeCalculator calculator)
		{
			"Given a set of calculators".Given(() => calculators = Params.Of(A.Fake<ICalculator>(), A.Fake<ICalculator>()));

			"When creating a calculator".When(() => calculator = new CompositeCalculator(calculators));

			"It has the specified calculations in order".Then(() => calculator.Calculators.SequenceEqual(calculators));
		}

		[Scenario]
		public void ApplyFunctionCalculator(Variable outputVariable, int value, Func<GraspRuntime, object> function, FunctionCalculator calculator, GraspSchema schema, GraspRuntime runtime)
		{
			"Given an output variable".Given(() => outputVariable = new Variable<int>("X"));
			"And a value".And(() => value = 1);
			"And a function which returns the value".And(() => function = runtime2 => value);
			"And a calculator for the function".And(() => calculator = new FunctionCalculator(outputVariable, function));
			"And an empty schema".And(() => schema = new GraspSchema());
			"And a runtime with the empty schema and function calculator".And(() => runtime = new GraspRuntime(schema, calculator));

			"When applying calculations to the runtime".When(() => calculator.ApplyCalculation(runtime));

			"It sets the output variable to the value".Then(() => runtime.GetVariableValue(outputVariable).Should().Be(value));
		}

		[Scenario]
		public void ApplyCompositeCalculator(
			Variable outputVariable1, Variable outputVariable2, int value1, int value2,
			Func<GraspRuntime, object> function1, Func<GraspRuntime, object> function2, FunctionCalculator calculator1, FunctionCalculator calculator2,
			CompositeCalculator compositeCalculator, GraspSchema schema, GraspRuntime runtime)
		{
			"Given an output variable".Given(() => outputVariable1 = new Variable<int>("X"));
			"And a second output variable".And(() => outputVariable2 = new Variable<int>("Y"));
			"And a value".And(() => value1 = 1);
			"And a second value".And(() => value2 = 2);
			"And a function which returns the first value".And(() => function1 = runtime2 => value1);
			"And a second function which returns the second value".And(() => function2 = runtime2 => value2);
			"And a calculator for the first function".And(() => calculator1 = new FunctionCalculator(outputVariable1, function1));
			"And a second calculator for the second function".And(() => calculator2 = new FunctionCalculator(outputVariable2, function2));
			"And a composite calculator for the first and second calculators".And(() => compositeCalculator = new CompositeCalculator(calculator1, calculator2));
			"And an empty schema".And(() => schema = new GraspSchema());
			"And a runtime with the empty schema and composite calculator".And(() => runtime = new GraspRuntime(schema, compositeCalculator));

			"When applying calculations to the runtime".When(() => compositeCalculator.ApplyCalculation(runtime));

			"It sets the first output variable to the first value".Then(() => runtime.GetVariableValue(outputVariable1).Should().Be(value1));
			"It sets the second output variable to the second value".Then(() => runtime.GetVariableValue(outputVariable2).Should().Be(value2));
		}
	}
}