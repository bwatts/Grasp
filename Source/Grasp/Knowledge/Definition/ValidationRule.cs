using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Definition
{
	public sealed class ValidationRule : Notion, IValidationRule
	{
		public static readonly Field<Identifier> OutputVariableIdentifierField = Field.On<ValidationRule>.For(x => x.OutputVariableIdentifier);
		public static readonly Field<Rule> RuleField = Field.On<ValidationRule>.For(x => x.Rule);

		public static readonly Namespace Namespace = new Namespace(Resources.ValidationNamespace);

		public ValidationRule(Identifier outputVariableIdentifier, Rule rule)
		{
			Contract.Requires(outputVariableIdentifier != null);
			Contract.Requires(rule != null);

			OutputVariableIdentifier = outputVariableIdentifier;
			Rule = rule;
		}

		public ValidationRule(string outputVariableIdentifier, Rule rule) : this(new Identifier(outputVariableIdentifier), rule)
		{}

		public Identifier OutputVariableIdentifier { get { return GetValue(OutputVariableIdentifierField); } private set { SetValue(OutputVariableIdentifierField, value); } }
		public Rule Rule { get { return GetValue(RuleField); } private set { SetValue(RuleField, value); } }

		public Calculation<bool> GetCalculation(Variable target)
		{
			return new Calculation<bool>(GetOutputVariableName(target), GetCalculationExpression(target));
		}

		private FullName GetOutputVariableName(Variable target)
		{
			return GetValidationNamespace(target) + OutputVariableIdentifier;
		}

		private Namespace GetValidationNamespace(Variable target)
		{
			return new Namespace(target.Name.ToString()) + Namespace;
		}

		private Expression GetCalculationExpression(Variable target)
		{
			return Expression.Invoke(GetLambdaExpression(target), target.ToExpression());
		}

		private LambdaExpression GetLambdaExpression(Variable target)
		{
			return Rule.ToLambdaExpression(target.Type);
		}
	}
}