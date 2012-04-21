using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis
{
	public class CreateCalculation : Behavior
	{
		Variable _outputVariable;
		Expression _expression;
		Calculation _calculation;

		protected override void Given()
		{
			_outputVariable = new Variable("Grasp", "Test", typeof(int));
			_expression = Expression.Constant(0);
		}

		protected override void When()
		{
			_calculation = new Calculation(_outputVariable, _expression);
		}

		[Then]
		public void HasOriginalOutputVariable()
		{
			Assert.That(_calculation.OutputVariable, Is.EqualTo(_outputVariable));
		}

		[Then]
		public void HasOriginalExpression()
		{
			Assert.That(_calculation.Expression, Is.EqualTo(_expression));
		}
	}
}