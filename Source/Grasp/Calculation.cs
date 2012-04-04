using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;

namespace Grasp
{
	public class Calculation
	{
		public Calculation(Variable outputVariable, Expression expression)
		{
			Contract.Requires(outputVariable != null);
			Contract.Requires(expression != null);

			OutputVariable = outputVariable;
			Expression = expression;
		}

		public Variable OutputVariable { get; private set; }

		public Expression Expression { get; private set; }

		public override string ToString()
		{
			return Resources.Calculation.FormatInvariant(OutputVariable, Expression);
		}
	}
}