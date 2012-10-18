using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis
{
	public class VariableBindings
	{
		[Scenario]
		public void Create(Variable variable, int value, VariableBinding binding)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And a value".And(() => value = 1);

			"When creating a binding".When(() => binding = new VariableBinding(variable, value));

			"It has the specified variable".Then(() => binding.Variable.Should().Be(variable));
			"It has the specified value".Then(() => binding.Value.Should().Be(value));
		}
	}
}