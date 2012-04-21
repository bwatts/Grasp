using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Analysis.Compilation
{
	/// <summary>
	/// Indicates a set of calculations contains a cycle
	/// </summary>
	public class CalculationCycleException : Exception
	{
		/// <summary>
		/// Initializes an exception with the specified calculation context and repeated calculation
		/// </summary>
		/// <param name="context">The calculations above the repeated one from the root</param>
		/// <param name="repeatedCalculation">The calculation repeated in the context of the root</param>
		public CalculationCycleException(IEnumerable<Calculation> context, Calculation repeatedCalculation)
		{
			Contract.Requires(context != null);
			Contract.Requires(repeatedCalculation != null);

			Context = context.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		/// <summary>
		/// Initializes an exception with the specified calculation contextm, repeated calculation, and message
		/// </summary>
		/// <param name="context">The calculations above the repeated one from the root</param>
		/// <param name="repeatedCalculation">The calculation repeated in the context of the root</param>
		/// <param name="message">The message that describes the error</param>
		public CalculationCycleException(IEnumerable<Calculation> context, Calculation repeatedCalculation, string message)
			: base(message)
		{
			Contract.Requires(context != null);
			Contract.Requires(repeatedCalculation != null);

			Context = context.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		/// <summary>
		/// Initializes an exception with the specified calculation context, repeated calculation, message, and inner exception
		/// </summary>
		/// <param name="context">The calculations above the repeated one from the root</param>
		/// <param name="repeatedCalculation">The calculation repeated in the context of the root</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public CalculationCycleException(IEnumerable<Calculation> context, Calculation repeatedCalculation, string message, Exception inner)
			: base(message, inner)
		{
			Contract.Requires(context != null);
			Contract.Requires(repeatedCalculation != null);

			Context = context.ToList().AsReadOnly();
			RepeatedCalculation = repeatedCalculation;
		}

		/// <summary>
		/// Gets the calculations above the repeated one from the root
		/// </summary>
		public ReadOnlyCollection<Calculation> Context { get; private set; }

		/// <summary>
		/// Gets the calculation repeated in the context of the root
		/// </summary>
		public Calculation RepeatedCalculation { get; private set; }
	}
}