using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public sealed class FunctionCalculator : ICalculator
	{
		public FunctionCalculator(Variable outputVariable, Func<GraspRuntime, object> function)
		{
			Contract.Requires(outputVariable != null);
			Contract.Requires(function != null);

			OutputVariable = outputVariable;
			Function = function;
		}

		public Variable OutputVariable { get; private set; }

		public Func<GraspRuntime, object> Function { get; private set; }

		public void ApplyCalculation(GraspRuntime runtime)
		{
			runtime.SetVariableValue(OutputVariable, Function(runtime));
		}
	}
}