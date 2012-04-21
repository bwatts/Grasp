using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Compilation
{
	/// <summary>
	/// Indiciates a calculation's result type is not assignable to its variable's output type
	/// </summary>
	public class InvalidCalculationResultTypeException : Exception
	{
		/// <summary>
		/// Initializes an exception with the specified calculation
		/// </summary>
		/// <param name="variable">The invalid calculation</param>
		public InvalidCalculationResultTypeException(Calculation calculation)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		/// <summary>
		/// Initializes an exception with the specified calculation and message
		/// </summary>
		/// <param name="variable">The invalid calculation</param>
		/// <param name="message">The message that describes the error</param>
		public InvalidCalculationResultTypeException(Calculation calculation, string message) : base(message)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		/// <summary>
		/// Initializes an exception with the specified calculation, message, and inner exception
		/// </summary>
		/// <param name="variable">The invalid calculation</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public InvalidCalculationResultTypeException(Calculation calculation, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		/// <summary>
		/// Gets the invalid calculation
		/// </summary>
		public Calculation Calculation { get; private set; }
	}
}