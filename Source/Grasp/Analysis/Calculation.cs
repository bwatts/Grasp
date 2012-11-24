using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;

namespace Grasp.Analysis
{
	/// <summary>
	/// The unit of logic in a schema
	/// </summary>
	public class Calculation : Notion
	{
		public static readonly Field<Variable> OutputVariableField = Field.On<Calculation>.For(x => x.OutputVariable);
		public static readonly Field<Expression> ExpressionField = Field.On<Calculation>.For(x => x.Expression);

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
		public Variable OutputVariable { get { return GetValue(OutputVariableField); } private set { SetValue(OutputVariableField, value); } }

		/// <summary>
		/// Gets the expression tree which defines the body of this calculation
		/// </summary>
		public Expression Expression { get { return GetValue(ExpressionField); } private set { SetValue(ExpressionField, value); } }

		/// <summary>
		/// Gets a textual representation of this calculation
		/// </summary>
		/// <returns>A textual representation of this calculation</returns>
		public override string ToString()
		{
			return Resources.Calculation.FormatInvariant(OutputVariable, Expression);
		}
	}
}