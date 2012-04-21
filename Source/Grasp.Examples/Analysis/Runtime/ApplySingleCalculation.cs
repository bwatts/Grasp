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
	public class ApplySingleCalculation : Behavior
	{
		int _left;
		int _right;
		Variable _outputVariable;
		GraspRuntime _runtime;

		protected override void Given()
		{
			_left = 1;
			_right = 2;

			_outputVariable = new Variable("Grasp", "Output", typeof(int));

			var calculation = new Calculation(_outputVariable, Expression.Add(Expression.Constant(_left), Expression.Constant(_right)));

			var schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { calculation });

			var executable = schema.Compile();

			_runtime = executable.GetRuntime(A.Fake<IRuntimeSnapshot>());
		}

		protected override void When()
		{
			_runtime.ApplyCalculations();
		}

		[Then]
		public void SetsOutputVariableToCorrectValue()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable), Is.EqualTo(_left + _right));
		}
	}
}