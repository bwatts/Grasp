using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis
{
	public class Calculations
	{
		[Scenario]
		public void Create(Variable outputVariable, Expression expression, Calculation calculation)
		{
			"Given an output variable".Given(() => outputVariable = new Variable<int>("X"));
			"And an expression".And(() => expression = Expression.Constant(0));

			"When creating a calculation".When(() => calculation = new Calculation(outputVariable, expression));

			"It has the specified output variable".Then(() => calculation.OutputVariable.Should().Be(outputVariable));
			"It has the specified expression".Then(() => calculation.Expression.Should().Be(expression));
		}

		[Scenario]
		public void GetText(Variable outputVariable, ConstantExpression expression, Calculation calculation, string text)
		{
			"Given an output variable".Given(() => outputVariable = new Variable<int>("X"));
			"And an expression".Given(() => expression = Expression.Constant(0));
			"And a calculation".And(() => calculation = new Calculation(outputVariable, expression));

			"When calling .ToString() on the calculation".When(() => text = calculation.ToString());

			"It is the assignment of the expression to the output variable"
				.Then(() => calculation.ToString().Should().Be(outputVariable.ToString() + " = " + expression.Value.ToString()));
		}
	}
}