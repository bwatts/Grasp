using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis.Variables
{
	public class Expressions
	{
		[Scenario]
		public void Create(Variable variable, VariableExpression expression)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));

			"When creating an expression".When(() => expression = Variable.Expression(variable));

			"It has the variable expression type".Then(() => expression.NodeType.Should().Be(VariableExpression.ExpressionType));
			"It has the specified variable".Then(() => expression.Variable.Should().Be(variable));
			"It has the specified variable's type".Then(() => expression.Type.Should().Be(variable.Type));
		}

		[Scenario]
		public void GetText(Variable variable, VariableExpression expression, string text)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And an expression".Given(() => expression = Variable.Expression(variable));

			"When calling .ToString() on the expression".When(() => text = expression.ToString());

			"The text is the variable's text".Then(() => text.Should().Be(variable.ToString()));
		}
	}
}