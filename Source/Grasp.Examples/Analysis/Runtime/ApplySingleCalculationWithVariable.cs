using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Analysis.Runtime
{
	public class ApplySingleCalculationWithVariable : Behavior
	{
		int _leftValue;
		int _right;
		Variable _outputVariable;
		GraspRuntime _runtime;

		protected override void Given()
		{
			_leftValue = 1;
			_right = 2;

			var left = new Variable("Grasp", "Left", typeof(int));

			_outputVariable = new Variable("Grasp", "Output", typeof(int));

			var calculation = new Calculation(_outputVariable, Expression.Add(Variable.Expression(left), Expression.Constant(_right)));

			var schema = new GraspSchema(new[] { left }, new[] { calculation });

			var executable = schema.Compile();

			var initialState = A.Fake<IRuntimeSnapshot>();

			A.CallTo(() => initialState.GetValue(left)).Returns(_leftValue);

			_runtime = executable.GetRuntime(initialState);
		}

		protected override void When()
		{
			_runtime.ApplyCalculations();
		}

		[Then]
		public void SetsOutputVariableToCorrectValue()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable), Is.EqualTo(_leftValue + _right));
		}
	}
}