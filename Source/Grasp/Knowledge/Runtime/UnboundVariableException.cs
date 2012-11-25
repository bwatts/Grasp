using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Runtime
{
	/// <summary>
	/// Indicates an attempt to retrieve the value of an unbound variable
	/// </summary>
	public class UnboundVariableException : GraspException
	{
		/// <summary>
		/// Initializes an exception with the specified variable
		/// </summary>
		/// <param name="name">The name of the unbound variable</param>
		public UnboundVariableException(FullName name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		/// <summary>
		/// Initializes an exception with the specified variable and message
		/// </summary>
		/// <param name="name">The name of the unbound variable</param>
		/// <param name="message">The message that describes the error</param>
		public UnboundVariableException(FullName name, string message) : base(message)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		/// <summary>
		/// Initializes an exception with the specified variable, message, and inner exception
		/// </summary>
		/// <param name="name">The name of the unbound variable</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public UnboundVariableException(FullName name, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		/// <summary>
		/// Gets name of the unbound variable
		/// </summary>
		public FullName Name { get; private set; }
	}
}