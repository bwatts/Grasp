using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Compilation
{
	public class GraspExecutable
	{
		public GraspExecutable(GraspSchema schema, ICalculator calculator)
		{
			Contract.Requires(schema != null);
			Contract.Requires(calculator != null);

			Schema = schema;
			Calculator = calculator;
		}

		public GraspSchema Schema { get; private set; }

		public ICalculator Calculator { get; private set; }

		public GraspRuntime GetRuntime(IRuntimeSnapshot initialState)
		{
			Contract.Requires(initialState != null);

			return new GraspRuntime(Schema, Calculator, GetBindings(initialState));
		}

		private IEnumerable<VariableBinding> GetBindings(IRuntimeSnapshot initialState)
		{
			return Schema.Variables.Select(variable => new VariableBinding(variable, initialState.GetValue(variable)));
		}
	}
}