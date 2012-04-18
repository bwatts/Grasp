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
		public CalculationCycleException(IEnumerable<Calculation> context, Calculation repeatedCalculation)
		{
			Contract.Requires(context != null);
			Contract.Requires(repeatedCalculation != null);

			Context = context.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		public CalculationCycleException(IEnumerable<Calculation> context, Calculation repeatedCalculation, string message)
			: base(message)
		{
			Contract.Requires(context != null);
			Contract.Requires(repeatedCalculation != null);

			Context = context.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		public CalculationCycleException(IEnumerable<Calculation> context, Calculation repeatedCalculation, string message, Exception inner)
			: base(message, inner)
		{
			Contract.Requires(context != null);
			Contract.Requires(repeatedCalculation != null);

			Context = context.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		public ReadOnlyCollection<Calculation> Context { get; private set; }

		public Calculation RepeatedCalculation { get; private set; }
	}
}