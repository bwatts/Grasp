using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;

namespace Grasp.Knowledge
{
	/// <summary>
	/// The unit of logic in a schema - maps an input expression to an output variable
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
			return Resources.Calculation.FormatInvariant(OutputVariable.Name, Expression);
		}
	}

	/// <summary>
	/// The unit of logic in a schema - maps an input expression to an output variable
	/// </summary>
	/// <typeparam name="TOutput">The type of the calculation's output variable</typeparam>
	public class Calculation<TOutput> : Calculation
	{
		public new static readonly Field<Variable<TOutput>> OutputVariableField = Field.On<Calculation<TOutput>>.For(x => x.OutputVariable);

		/// <summary>
		/// Initializes a calculation with the specified output variable and expression
		/// </summary>
		/// <param name="outputVariable">The variable to which the output of this calculation is assigned</param>
		/// <param name="expression">The expression tree which defines the body of this calculation</param>
		public Calculation(Variable<TOutput> outputVariable, Expression expression) : base(outputVariable, expression)
		{
			OutputVariable = outputVariable;
		}

		/// <summary>
		/// Gets the variable to which the output of this calculation is assigned
		/// </summary>
		public new Variable<TOutput> OutputVariable { get { return GetValue(OutputVariableField); } private set { SetValue(OutputVariableField, value); } }
	}
}