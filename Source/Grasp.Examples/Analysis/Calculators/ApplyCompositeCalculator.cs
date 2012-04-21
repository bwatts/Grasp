using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis.Calculators
{
	public class ApplyCompositeCalculator : Behavior
	{
		Variable _outputVariable1;
		Variable _outputVariable2;
		int _value1;
		int _value2;
		Func<GraspRuntime, object> _function1;
		Func<GraspRuntime, object> _function2;
		GraspRuntime _runtime;
		FunctionCalculator _calculator1;
		FunctionCalculator _calculator2;
		CompositeCalculator _compositeCalculator;

		protected override void Given()
		{
			_outputVariable1 = new Variable("Grasp", "Test1", typeof(int));
			_outputVariable2 = new Variable("Grasp", "Test2", typeof(int));

			_value1 = 1;
			_value2 = 2;

			_function1 = runtime => _value1;
			_function2 = runtime => _value2;

			_calculator1 = new FunctionCalculator(_outputVariable1, _function1);
			_calculator2 = new FunctionCalculator(_outputVariable2, _function2);

			_compositeCalculator = new CompositeCalculator(_calculator1, _calculator2);

			var schema = new GraspSchema(Enumerable.Empty<Variable>(), Enumerable.Empty<Calculation>());

			_runtime = new GraspRuntime(schema, _compositeCalculator, Enumerable.Empty<VariableBinding>());
		}

		protected override void When()
		{
			_compositeCalculator.ApplyCalculation(_runtime);
		}

		[Then]
		public void SetsOutputVariable1()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable1), Is.EqualTo(_value1));
		}

		[Then]
		public void SetsOutputVariable2()
		{
			Assert.That(_runtime.GetVariableValue(_outputVariable2), Is.EqualTo(_value2));
		}
	}
}