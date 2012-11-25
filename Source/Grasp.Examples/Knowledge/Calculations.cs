using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge
{
	public class Calculations
	{
		[Fact] public void Create()
		{
			var outputVariable = new Variable<int>("X");
			var expression = Expression.Constant(0);

			var calculation = new Calculation(outputVariable, expression);

			calculation.OutputVariable.Should().Be(outputVariable);
			calculation.Expression.Should().Be(expression);
		}

		[Fact] public void CreateGeneric()
		{
			var outputVariable = new Variable<int>("X");
			var expression = Expression.Constant(0);

			var calculation = new Calculation<int>(outputVariable, expression);

			((Calculation) calculation).OutputVariable.Should().Be(outputVariable);
			calculation.OutputVariable.Should().Be(outputVariable);
			calculation.Expression.Should().Be(expression);
		}

		[Fact] public void GetText()
		{
			var calculation = new Calculation(new Variable<int>("X"), Expression.Constant(0));

			var text = calculation.ToString();

			calculation.ToString().Should().Be("X = 0");
		}
	}
}