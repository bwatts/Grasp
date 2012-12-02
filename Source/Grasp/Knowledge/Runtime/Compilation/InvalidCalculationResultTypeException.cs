using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	/// <summary>
	/// Indiciates a calculation's result type is not assignable to its variable's output type
	/// </summary>
	public class InvalidCalculationResultTypeException : CompilationException
	{
		/// <summary>
		/// Initializes an exception with the specified calculation
		/// </summary>
		/// <param name="schema">The schema being compiled</param>
		/// <param name="calculation">The invalid calculation</param>
		public InvalidCalculationResultTypeException(Schema schema, Calculation calculation) : base(schema)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		/// <summary>
		/// Initializes an exception with the specified calculation and message
		/// </summary>
		/// <param name="schema">The schema being compiled</param>
		/// <param name="calculation">The invalid calculation</param>
		/// <param name="message">The message that describes the error</param>
		public InvalidCalculationResultTypeException(Schema schema, Calculation calculation, string message) : base(schema, message)
		{
			Contract.Requires(calculation != null);

			Calculation = calculation;
		}

		/// <summary>
		/// Initializes an exception with the specified calculation, message, and inner exception
		/// </summary>
		/// <param name="schema">The schema being compiled</param>
		/// <param name="calculation">The invalid calculation</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public InvalidCalculationResultTypeException(Schema schema, Calculation calculation, string message, Exception inner) : base(schema, message, inner)
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