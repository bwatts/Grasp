using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Compilation
{
	public class CalculationCycleException : Exception
	{
		public CalculationCycleException(IEnumerable<Calculation> calculations, Calculation repeatedCalculation)
		{
			Contract.Requires(calculations != null);
			Contract.Requires(repeatedCalculation != null);

			Calculations = calculations.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		public CalculationCycleException(IEnumerable<Calculation> calculations, Calculation repeatedCalculation, string message)
			: base(message)
		{
			Contract.Requires(calculations != null);
			Contract.Requires(repeatedCalculation != null);

			Calculations = calculations.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		public CalculationCycleException(IEnumerable<Calculation> calculations, Calculation repeatedCalculation, string message, Exception inner)
			: base(message, inner)
		{
			Contract.Requires(calculations != null);
			Contract.Requires(repeatedCalculation != null);

			Calculations = calculations.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		public ReadOnlyCollection<Calculation> Calculations { get; private set; }

		public Calculation RepeatedCalculation { get; private set; }
	}
}