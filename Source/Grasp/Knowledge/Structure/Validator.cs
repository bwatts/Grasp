using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Structure
{
	public sealed class Validator : Notion, IValidator
	{
		public static readonly Field<Identifier> OutputVariableIdentifierField = Field.On<Validator>.For(x => x.OutputVariableIdentifier);
		public static readonly Field<Expression> ExpressionField = Field.On<Validator>.For(x => x.Expression);

		public Validator(Identifier outputVariableIdentifier, Expression expression)
		{
			Contract.Requires(outputVariableIdentifier != null);
			Contract.Requires(expression != null);
			Contract.Requires(expression.Type == typeof(bool));

			OutputVariableIdentifier = outputVariableIdentifier;
			Expression = expression;
		}

		public Validator(string outputVariableIdentifier, Expression expression) : this(new Identifier(outputVariableIdentifier), expression)
		{}

		public Identifier OutputVariableIdentifier { get { return GetValue(OutputVariableIdentifierField); } private set { SetValue(OutputVariableIdentifierField, value); } }
		public Expression Expression { get { return GetValue(ExpressionField); } private set { SetValue(ExpressionField, value); } }

		public IValidationRule GetRule(Variable target)
		{
			return new ValueRule(target, OutputVariableIdentifier, Expression);
		}
	}
}