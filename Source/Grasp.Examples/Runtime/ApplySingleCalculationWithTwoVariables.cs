using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Runtime
{
	public class ApplySingleCalculationWithTwoVariables : Behavior
	{
		int _leftValue;
		int _rightValue;
		Variable _outputVariable;
		GraspRuntime _runtime;

		protected override void Given()
		{
			_leftValue = 1;
			_rightValue = 2;

			var left = new Variable("Grasp", "Left", typeof(int));
			var right = new Variable("Grasp", "Right", typeof(int));

			_outputVariable = new Variable("Grasp", "Output", typeof(int));

			var calculation = new Calculation(_outputVariable, Expression.Add(Variable.Expression(left), Variable.Expression(right)));

			var schema = new GraspSchema(new[] { left, right }, new[] { calculation });

			var executable = schema.Compile();

			var initialState = A.Fake<IRuntimeSnapshot>();

			A.CallTo(() => initialState.GetValue(left)).Returns(_leftValue);
			A.CallTo(() => initialState.GetValue(right)).Returns(_rightValue);

			_runtime = executable.GetRuntime(initialState);
		}

		protected override void When()
		{
			_runtime.ApplyCalculations();
		}

		[Then]
		public void SetsOutputVariableToCorrectValue()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable), Is.EqualTo(_leftValue + _rightValue));
		}
	}
}