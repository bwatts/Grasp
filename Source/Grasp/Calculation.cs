using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;

namespace Grasp
{
	/// <summary>
	/// The unit of logic in a schema
	/// </summary>
	public class Calculation
	{
		/// <summary>
		/// Initializes a calculation with the specified output variable and expression
		/// </summary>
		/// <param name="outputVariable">The variable to which the output of this calculation is assigned</param>
		/// <param name="expression">The expression tree which defines the body of this calculation</param>
		public Calculation(Variable outputVariable, Expression expression)
		{
			Contract.Requires(outputVariable != null);
			Contract.Requires(expression != null);

			OutputVariable = outputVariable;
			Expression = expression;
		}

		/// <summary>
		/// Gets the variable to which the output of this calculation is assigned
		/// </summary>
		public Variable OutputVariable { get; private set; }

		/// <summary>
		/// Gets the expression tree which defines the body of this calculation
		/// </summary>
		public Expression Expression { get; private set; }

		/// <summary>
		/// Gets a text representation of this calculation
		/// </summary>
		public override string ToString()
		{
			return Resources.Calculation.FormatInvariant(OutputVariable, Expression);
		}
	}
}