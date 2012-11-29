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
			var name = new FullName("A");
			var value = 1;

			var binding = new VariableBinding(name, value);

			((object) binding.Name).Should().Be(name);
			binding.Value.Should().Be(value);
		}

		[Fact] public void Set()
		{
			var binding = new VariableBinding("A", 1);
			var newValue = 2;

			binding.Value = newValue;

			binding.Value.Should().Be(newValue);
		}
	}
}