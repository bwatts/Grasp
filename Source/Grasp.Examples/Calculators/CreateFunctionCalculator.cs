using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Calculators
{
	public class CreateFunctionCalculator : Behavior
	{
		Variable _outputVariable;
		Func<GraspRuntime, object> _function;
		FunctionCalculator _calculator;

		protected override void Given()
		{
			_outputVariable = new Variable("Grasp", "Test", typeof(int));

			_function = runtime => 1;
		}

		protected override void When()
		{
			_calculator = new FunctionCalculator(_outputVariable, _function);
		}

		[Then]
		public void HasOriginalOutputVariable()
		{
			Assert.That(_calculator.OutputVariable, Is.EqualTo(_outputVariable));
		}

		[Then]
		public void HasOriginalFunction()
		{
			Assert.That(_calculator.Function, Is.EqualTo(_function));
		}
	}
}