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
	public class ApplyMultipleCalculations : Behavior
	{
		int _left1;
		int _right1;
		int _left2;
		int _right2;
		Variable _outputVariable1;
		Variable _outputVariable2;
		GraspRuntime _runtime;

		protected override void Given()
		{
			_left1 = 1;
			_right1 = 2;

			_left2 = 11;
			_right2 = 12;

			_outputVariable1 = new Variable("Grasp", "Output1", typeof(int));
			_outputVariable2 = new Variable("Grasp", "Output2", typeof(int));

			var calculation1 = new Calculation(_outputVariable1, Expression.Add(Expression.Constant(_left1), Expression.Constant(_right1)));
			var calculation2 = new Calculation(_outputVariable2, Expression.Add(Expression.Constant(_left2), Expression.Constant(_right2)));

			var schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { calculation1, calculation2 });

			var executable = schema.Compile();

			_runtime = executable.GetRuntime(A.Fake<IRuntimeSnapshot>());
		}

		protected override void When()
		{
			_runtime.ApplyCalculations();
		}

		[Then]
		public void SetsOutputVariable1ToCorrectValue()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable1), Is.EqualTo(_left1 + _right1));
		}

		[Then]
		public void SetsOutputVariable2ToCorrectValue()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable2), Is.EqualTo(_left2 + _right2));
		}
	}
}