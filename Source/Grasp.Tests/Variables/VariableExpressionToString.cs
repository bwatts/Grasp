using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Variables
{
	public class VariableExpressionToString : Behavior
	{
		Variable _variable;
		VariableExpression _expression;
		string _expressionText;

		protected override void Given()
		{
			_variable = new Variable("TestNamespace", "Test", typeof(int));
			_expression = Variable.Expression(_variable);
		}

		protected override void When()
		{
			_expressionText = _expression.ToString();
		}

		[Then]
		public void IsFullyQualified()
		{
			Assert.That(_expressionText, Is.EqualTo(_variable.ToString()));
		}
	}
}