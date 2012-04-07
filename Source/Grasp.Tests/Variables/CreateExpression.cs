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
		public void VariableIsOriginal()
		{
			Assert.That(_expression.Variable, Is.EqualTo(_variable));
		}
	}
}