using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Compilation
{
	public class InvalidCalculationVariableException : Exception
	{
		public InvalidCalculationVariableException(Variable variable, Calculation calculation)
		{
			Contract.Requires(variable != null);
			Contract.Requires(calculation != null);

			Variable = variable;
			Calculation = calculation;
		}

		public InvalidCalculationVariableException(Variable variable, Calculation calculation, string message)
			: base(message)
		{
			Contract.Requires(variable != null);
			Contract.Requires(calculation != null);

			Variable = variable;
			Calculation = calculation;
		}

		public InvalidCalculationVariableException(Variable variable, Calculation calculation, string message, Exception inner)
			: base(message, inner)
		{
			Contract.Requires(variable != null);
			Contract.Requires(calculation != null);

			Variable = variable;
			Calculation = calculation;
		}

		public Variable Variable { get; private set; }

		public Calculation Calculation { get; private set; }
	}
}