using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Analysis.Variables
{
	public class Bindings
	{
		[Fact] public void Create()
		{
			var variable = new Variable<int>("X");
			var value = 1;

			var binding = new VariableBinding(variable, value);

			binding.Variable.Should().Be(variable);
			binding.Value.Should().Be(value);
		}
	}
}