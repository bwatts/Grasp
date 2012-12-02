using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	public sealed class ValueRule : Notion, IValidationRule
	{
		public static readonly Field<Variable> VariableField = Field.On<ValueRule>.For(x => x.Variable);
		public static readonly Field<Identifier> OutputVariableIdentifierField = Field.On<ValueRule>.For(x => x.OutputVariableIdentifier);
		public static readonly Field<Expression> ExpressionField = Field.On<ValueRule>.For(x => x.Expression);

		public ValueRule(Variable variable, Identifier outputVariableIdentifier, Expression expression)
		{
			Contract.Requires(variable != null);
			Contract.Requires(outputVariableIdentifier != null);
			Contract.Requires(expression != null);
			Contract.Requires(expression.Type == typeof(bool));

			Variable = variable;
			OutputVariableIdentifier = outputVariableIdentifier;
			Expression = expression;
		}

		public Variable Variable { get { return GetValue(VariableField); } private set { SetValue(VariableField, value); } }
		public Identifier OutputVariableIdentifier { get { return GetValue(OutputVariableIdentifierField); } private set { SetValue(OutputVariableIdentifierField, value); } }
		public Expression Expression { get { return GetValue(ExpressionField); } private set { SetValue(ExpressionField, value); } }

		public Calculation<bool> GetCalculation()
		{
			return new Calculation<bool>(GetOutputVariableName(), Expression);
		}

		private FullName GetOutputVariableName()
		{
			return ValidationRule.GetName(Variable.Name, OutputVariableIdentifier);
		}
	}
}