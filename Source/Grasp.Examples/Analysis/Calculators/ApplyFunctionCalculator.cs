using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis.Calculators
{
	public class ApplyFunctionCalculator : Behavior
	{
		Variable _outputVariable;
		int _value;
		Func<GraspRuntime, object> _function;
		GraspRuntime _runtime;
		FunctionCalculator _calculator;

		protected override void Given()
		{
			_outputVariable = new Variable("Grasp", "Test", typeof(int));

			_value = 1;

			_function = runtime => _value;

			_calculator = new FunctionCalculator(_outputVariable, _function);

			var schema = new GraspSchema(Enumerable.Empty<Variable>(), Enumerable.Empty<Calculation>());

			_runtime = new GraspRuntime(schema, _calculator, Enumerable.Empty<VariableBinding>());
		}

		protected override void When()
		{
			_calculator.ApplyCalculation(_runtime);
		}

		[Then]
		public void SetsOutputVariable()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable), Is.EqualTo(_value));
		}
	}
}