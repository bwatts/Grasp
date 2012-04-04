using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public class VariableBinding
	{
		public VariableBinding(Variable variable, object value)
		{
			Contract.Requires(variable != null);

			// TODO: Ensure that the value is assignable to the variable type

			Variable = variable;
			Value = value;
		}

		public Variable Variable { get; private set; }

		public object Value { get; set; }
	}
}