using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp
{
	public class UnboundVariableException : Exception
	{
		public UnboundVariableException(Variable variable)
		{
			Contract.Requires(variable != null);

			Variable = variable;
		}

		public UnboundVariableException(Variable variable, string message) : base(message)
		{
			Contract.Requires(variable != null);

			Variable = variable;
		}

		public UnboundVariableException(Variable variable, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(variable != null);

			Variable = variable;
		}

		public Variable Variable { get; private set; }
	}
}