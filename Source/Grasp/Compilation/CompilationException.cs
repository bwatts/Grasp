using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Compilation
{
	public class CompilationException : Exception
	{
		public CompilationException(GraspSchema schema)
		{
			Contract.Requires(schema != null);

			Schema = schema;
		}

		public CompilationException(GraspSchema schema, string message) : base(message)
		{
			Contract.Requires(schema != null);

			Schema = schema;
		}

		public CompilationException(GraspSchema schema, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(schema != null);

			Schema = schema;
		}

		public GraspSchema Schema { get; private set; }
	}
}