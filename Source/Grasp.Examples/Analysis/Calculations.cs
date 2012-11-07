using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Analysis
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

		[Fact] public void GetText()
		{
			var outputVariable = new Variable<int>("X");
			var expression = Expression.Constant(0);
			var calculation = new Calculation(outputVariable, expression);

			var text = calculation.ToString();

			calculation.ToString().Should().Be(outputVariable.ToString() + " = " + expression.Value.ToString());
		}
	}
}