using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge.Runtime
{
	public class SchemaBindings
	{
		[Fact] public void Create()
		{
			var schema = Schema.Empty;
			var calculator = A.Fake<ICalculator>();

			var binding = new SchemaBinding(schema, calculator);

			binding.Schema.Should().Be(schema);
			SchemaBinding._calculatorField.Get(binding).Should().Be(calculator);
		}

		[Fact] public void GetBoundVariableValue()
		{
			var variable = new Variable<int>("A");
			var schema = new Schema(Params.Of(variable));
			var value = 1;
			var binding = new SchemaBinding(schema, A.Fake<ICalculator>(), new VariableBinding(variable.Name, value));

			var boundValue = binding.GetVariableValue(variable.Name);

			boundValue.Should().Be(value);
		}

		[Fact] public void GetUnboundVariableValue()
		{
			var variable = new Variable<int>("A");
			var schema = new Schema(Params.Of(variable));
			var binding = new SchemaBinding(schema, A.Fake<ICalculator>());

			Assert.Throws<UnboundVariableException>(() => binding.GetVariableValue(variable.Name));
		}

		[Fact] public void SetBoundVariableValue()
		{
			var variable = new Variable<int>("A");
			var schema = new Schema(Params.Of(variable));
			var variableBinding = new VariableBinding(variable.Name, 1);
			var binding = new SchemaBinding(schema, A.Fake<ICalculator>(), variableBinding);
			var newValue = 2;

			binding.SetVariableValue(variable.Name, newValue);

			variableBinding.Value.Should().Be(newValue);
		}

		[Fact] public void SetUnboundVariableValue()
		{
			var variable = new Variable<int>("A");
			var schema = new Schema(Params.Of(variable));
			var binding = new SchemaBinding(schema, A.Fake<ICalculator>());
			var newValue = 1;

			binding.SetVariableValue(variable.Name, newValue);

			binding.GetVariableValue(variable.Name).Should().Be(newValue);
		}
	}
}