using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge.Runtime
{
	public class VariableBindings
	{
		[Fact] public void Create()
		{
			var variable = new Variable<int>("X");
			var value = 1;

			var binding = new VariableBinding(variable, value);

			binding.Variable.Should().Be(variable);
			binding.Value.Should().Be(value);
		}

		[Fact] public void Set()
		{
			var variable = new Variable<int>("X");
			var binding = new VariableBinding(variable, 1);
			var newValue = 2;

			binding.Value = newValue;

			binding.Value.Should().Be(newValue);
		}
	}
}