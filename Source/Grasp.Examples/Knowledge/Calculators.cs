using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Grasp.Knowledge.Runtime;
using Xunit;

namespace Grasp.Knowledge
{
	public class Calculators
	{
		[Fact] public void CreateFunctionCalculator()
		{
			var outputVariable = new Variable<int>("X");
			Func<SchemaBinding, object> function = schemaBinding => 1;

			var calculator = new FunctionCalculator(outputVariable, function);

			FunctionCalculator._outputVariableField.Get(calculator).Should().Be(outputVariable);
			FunctionCalculator._functionField.Get(calculator).Should().Be(function);
		}

		[Fact] public void CreateCompositeCalculator()
		{
			var calculators = Params.Of(A.Fake<ICalculator>(), A.Fake<ICalculator>());

			var calculator = new CompositeCalculator(calculators);

			CompositeCalculator._calculatorsField.Get(calculator).SequenceEqual(calculators);
		}

		[Fact] public void ApplyFunctionCalculator()
		{
			var outputVariable = new Variable<int>("X");
			var value = 1;
			var calculator = new FunctionCalculator(outputVariable, binding => value);
			var schema = new Schema();
			var schemaBinding = new SchemaBinding(schema, calculator);

			calculator.ApplyCalculation(schemaBinding);

			schemaBinding.GetVariableValue(outputVariable.Name).Should().Be(value);
		}

		[Fact] public void ApplyCompositeCalculator()
		{
			var value1 = 1;
			var value2 = 2;
			var outputVariable1 = new Variable<int>("X");
			var outputVariable2 = new Variable<int>("Y");
			var calculator1 = new FunctionCalculator(outputVariable1, binding1 => value1);
			var calculator2 = new FunctionCalculator(outputVariable2, binding2 => value2);
			var compositeCalculator = new CompositeCalculator(calculator1, calculator2);
			var schemaBinding = new SchemaBinding(new Schema(), compositeCalculator);

			compositeCalculator.ApplyCalculation(schemaBinding);

			schemaBinding.GetVariableValue(outputVariable1.Name).Should().Be(value1);
			schemaBinding.GetVariableValue(outputVariable2.Name).Should().Be(value2);
		}
	}
}