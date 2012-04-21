using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// Indicates an attempt to retrieve the value of an unbound variable
	/// </summary>
	public class UnboundVariableException : Exception
	{
		/// <summary>
		/// Initializes an exception with the specified variable
		/// </summary>
		/// <param name="variable">The unbound variable</param>
		public UnboundVariableException(Variable variable)
		{
			Contract.Requires(variable != null);

			Variable = variable;
		}

		/// <summary>
		/// Initializes an exception with the specified variable and message
		/// </summary>
		/// <param name="variable">The unbound variable</param>
		/// <param name="message">The message that describes the error</param>
		public UnboundVariableException(Variable variable, string message) : base(message)
		{
			Contract.Requires(variable != null);

			Variable = variable;
		}

		/// <summary>
		/// Initializes an exception with the specified variable, message, and inner exception
		/// </summary>
		/// <param name="variable">The unbound variable</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public UnboundVariableException(Variable variable, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(variable != null);

			Variable = variable;
		}

		/// <summary>
		/// Gets the unbound variable
		/// </summary>
		public Variable Variable { get; private set; }
	}
}