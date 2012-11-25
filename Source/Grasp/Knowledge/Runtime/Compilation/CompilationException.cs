using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	/// <summary>
	/// Indicates an error during compilation of a schema
	/// </summary>
	public class CompilationException : Exception
	{
		/// <summary>
		/// Initializes an exception with the specified schema
		/// </summary>
		/// <param name="schema">The schema whose compilation caused the error</param>
		public CompilationException(Schema schema)
		{
			Contract.Requires(schema != null);

			Schema = schema;
		}

		/// <summary>
		/// Initializes an exception with the specified schema, message, and inner exception
		/// </summary>
		/// <param name="schema">The schema whose compilation caused the error</param>
		/// <param name="message">The message that describes the error</param>
		public CompilationException(Schema schema, string message) : base(message)
		{
			Contract.Requires(schema != null);

			Schema = schema;
		}

		/// <summary>
		/// Initializes an exception with the specified schema, message, and inner exception
		/// </summary>
		/// <param name="schema">The schema whose compilation caused the error</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public CompilationException(Schema schema, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(schema != null);

			Schema = schema;
		}

		/// <summary>
		/// Gets the schema whose compilation caused the error
		/// </summary>
		public Schema Schema { get; private set; }
	}
}