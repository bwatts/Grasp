using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using Cloak.Linq;
using Grasp.Checks.Rules;

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
		/// Creates a calculation that applies the specified rule to the specified target variable
		/// </summary>
		/// <param name="targetVariable">The variable to which the rule applies</param>
		/// <param name="rule">The rule to apply to the variable</param>
		/// <param name="outputVariableName">The name of the boolean variable to which to assign the result</param>
		/// <param name="defaultResult">The result if the specified rule does not contain any checks</param>
		/// <returns>A calculation that applies the specified rule to the specified target variable</returns>
		public static Calculation<bool> FromRule(Variable targetVariable, Rule rule, FullName outputVariableName, bool defaultResult = true)
		{
			Contract.Requires(targetVariable != null);
			Contract.Requires(rule != null);
			Contract.Requires(outputVariableName != null);

			var lambda = rule.ToLambdaExpression(targetVariable.Type, defaultResult);

			var invokeLambdaWithTarget = Expression.Invoke(lambda, targetVariable.ToExpression());

			return new Calculation<bool>(outputVariableName, invokeLambdaWithTarget);
		}

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
			return new CalculationToString().GetText(this);
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
		/// Initializes a calculation with the specified output variable and expression
		/// </summary>
		/// <param name="outputVariableName">The name of the variable to which the output of this calculation is assigned</param>
		/// <param name="expression">The expression tree which defines the body of this calculation</param>
		public Calculation(FullName outputVariableName, Expression expression) : this(new Variable<TOutput>(outputVariableName), expression)
		{}

		/// <summary>
		/// Initializes a calculation with the specified output variable and expression
		/// </summary>
		/// <param name="outputVariableName">The name of the variable to which the output of this calculation is assigned</param>
		/// <param name="expression">The expression tree which defines the body of this calculation</param>
		public Calculation(string outputVariableName, Expression expression) : this(new Variable<TOutput>(outputVariableName), expression)
		{}

		/// <summary>
		/// Gets the variable to which the output of this calculation is assigned
		/// </summary>
		public new Variable<TOutput> OutputVariable { get { return GetValue(OutputVariableField); } private set { SetValue(OutputVariableField, value); } }
	}
}