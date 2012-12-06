using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge
{
	public class Variables
	{
		[Fact] public void Create()
		{
			var type = typeof(int);
			var name = new FullName("A");

			var variable = new Variable(type, name);

			variable.Type.Should().Be(type);
			((object) variable.Name).Should().Be(name);
		}

		[Fact] public void CreateGeneric()
		{
			var name = new FullName("A");

			var variable = new Variable<int>(name);

			variable.Type.Should().Be(typeof(int));
			((object) variable.Name).Should().Be(name);
		}

		[Fact] public void GetText()
		{
			var variable = new Variable<int>("A.B");

			var text = variable.ToString();

			text.Should().Be("A.B");
		}

		[Fact] public void ToExpression()
		{
			var variable = new Variable<int>("A");

			var expression = variable.ToExpression();

			expression.NodeType.Should().Be(VariableExpression.ExpressionType);
			expression.Variable.Should().Be(variable);
			expression.Type.Should().Be(variable.Type);
		}

		[Fact] public void GetExpressionText()
		{
			var variable = new Variable<int>("A");
			var expression = variable.ToExpression();

			var text = expression.ToString();

			text.Should().Be(variable.ToString());
		}
	}
}