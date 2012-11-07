using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Analysis.Variables
{
	public class Expressions
	{
		[Fact] public void Create()
		{
			var variable = new Variable<int>("X");

			var expression = Variable.Expression(variable);

			expression.NodeType.Should().Be(VariableExpression.ExpressionType);
			expression.Variable.Should().Be(variable);
			expression.Type.Should().Be(variable.Type);
		}

		[Fact] public void GetText()
		{
			var variable = new Variable<int>("X");
			var expression = Variable.Expression(variable);

			var text = expression.ToString();

			text.Should().Be(variable.ToString());
		}
	}
}