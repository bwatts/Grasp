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
	public class Runtimes
	{
		[Scenario]
		public void Create(GraspSchema schema, ICalculator calculator, GraspRuntime runtime)
		{
			"Given an empty schema".And(() => schema = new GraspSchema());
			"And a calculator".And(() => calculator = A.Fake<ICalculator>());

			"When creating a runtime".When(() => runtime = new GraspRuntime(schema, calculator));

			"It has the specified schema".Then(() => runtime.Schema.Should().Be(schema));
			"It has the specified calculator".Then(() => runtime.Calculator.Should().Be(calculator));
		}

		[Scenario]
		public void GetBoundVariableValue(Variable variable, GraspSchema schema, int value, GraspRuntime runtime, object runtimeValue)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And a schema with that variable".And(() => schema = new GraspSchema(Params.Of(variable)));
			"And a value".And(() => value = 1);
			"And a runtime with the variable bound to the value".And(() => runtime = new GraspRuntime(schema, A.Fake<ICalculator>(), new VariableBinding(variable, value)));

			"When getting the value of the variable from the runtime".When(() => runtimeValue = runtime.GetVariableValue(variable));

			"It has the bound value".Then(() => runtimeValue.Should().Be(value));
		}

		[Scenario]
		public void GetUnboundVariableValue(Variable variable, GraspSchema schema, GraspRuntime runtime, UnboundVariableException exception)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And a schema with that variable".And(() => schema = new GraspSchema(Params.Of(variable)));
			"And a runtime".And(() => runtime = new GraspRuntime(schema, A.Fake<ICalculator>()));

			"When getting the value of the variable from the runtime".When(() =>
			{
				try
				{
					runtime.GetVariableValue(variable);
				}
				catch(UnboundVariableException ex)
				{
					exception = ex;
				}
			});

			"It throws an exception".Then(() => exception.Should().NotBeNull());
		}

		[Scenario]
		public void SetBoundVariableValue(Variable variable, GraspSchema schema, VariableBinding binding, GraspRuntime runtime, int newValue)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And a schema with that variable".And(() => schema = new GraspSchema(Params.Of(variable)));
			"And a binding of that variable to a value".And(() => binding = new VariableBinding(variable, 1));
			"And a runtime with the binding".And(() => runtime = new GraspRuntime(schema, A.Fake<ICalculator>(), binding));
			"And a new value".And(() => newValue = 2);

			"When setting the variable to the new value".When(() => runtime.SetVariableValue(variable, newValue));

			"The binding is updated".Then(() => binding.Value.Should().Be(newValue));
		}

		[Scenario]
		public void SetUnboundVariableValue(Variable variable, GraspSchema schema, GraspRuntime runtime, int newValue)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And a schema with that variable".And(() => schema = new GraspSchema(Params.Of(variable)));
			"And a runtime the schema".And(() => runtime = new GraspRuntime(schema, A.Fake<ICalculator>()));
			"And a new value".And(() => newValue = 1);

			"When setting the variable to the new value".When(() => runtime.SetVariableValue(variable, newValue));

			"The binding is updated".Then(() => runtime.GetVariableValue(variable).Should().Be(newValue));
		}
	}
}