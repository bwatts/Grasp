using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Compilation
{
	public class InvalidCalculationResultTypeException : Exception
	{
		public InvalidCalculationResultTypeException(Calculation calculation)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		public InvalidCalculationResultTypeException(Calculation calculation, string message) : base(message)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		public InvalidCalculationResultTypeException(Calculation calculation, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		public Calculation Calculation { get; private set; }
	}
}