using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Variables
{
	public class CreateExpression : Behavior
	{
		Variable _variable;
		VariableExpression _expression;

		protected override void Given()
		{
			_variable = new Variable("Grasp", "Test", typeof(int));
		}

		protected override void When()
		{
			_expression = Variable.Expression(_variable);
		}

		[Then]
		public void NodeTypeIsVariableExpressionType()
		{
			Assert.That(_expression.NodeType, Is.EqualTo(VariableExpression.ExpressionType));
		}

		[Then]
		public void TypeIsVariableType()
		{
			Assert.That(_expression.Type, Is.EqualTo(_variable.Type));
		}

		[Then]
		public void HasOriginalVariable()
		{
			Assert.That(_expression.Variable, Is.EqualTo(_variable));
		}
	}
}