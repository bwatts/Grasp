using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	public sealed class ValidationRule : Notion, IValidationRule
	{
		public static readonly Field<Namespace> RootNamespaceField = Field.On<ValidationRule>.For(x => x.RootNamespace);
		public static readonly Field<Identifier> OutputVariableIdentifierField = Field.On<ValidationRule>.For(x => x.OutputVariableIdentifier);
		public static readonly Field<Expression> ExpressionField = Field.On<ValidationRule>.For(x => x.Expression);

		public static readonly Namespace Namespace = new Namespace(Resources.ValidationNamespace);

		public static FullName GetName(FullName variableName, Identifier ruleIdentifier)
		{
			Contract.Requires(variableName != null);
			Contract.Requires(ruleIdentifier != null);

			return variableName.ToNamespace() + Namespace + ruleIdentifier;
		}

		public ValidationRule(Namespace rootNamespace, Identifier outputVariableIdentifier, Expression expression)
		{
			Contract.Requires(rootNamespace != null);
			Contract.Requires(outputVariableIdentifier != null);
			Contract.Requires(expression != null);
			Contract.Requires(expression.Type == typeof(bool));

			RootNamespace = rootNamespace;
			OutputVariableIdentifier = outputVariableIdentifier;
			Expression = expression;
		}

		public Namespace RootNamespace { get { return GetValue(RootNamespaceField); } private set { SetValue(RootNamespaceField, value); } }
		public Identifier OutputVariableIdentifier { get { return GetValue(OutputVariableIdentifierField); } private set { SetValue(OutputVariableIdentifierField, value); } }
		public Expression Expression { get { return GetValue(ExpressionField); } private set { SetValue(ExpressionField, value); } }

		public Calculation<bool> GetCalculation()
		{
			return new Calculation<bool>(GetOutputVariableName(), Expression);
		}

		private FullName GetOutputVariableName()
		{
			return GetName(RootNamespace.ToFullName(), OutputVariableIdentifier);
		}
	}
}