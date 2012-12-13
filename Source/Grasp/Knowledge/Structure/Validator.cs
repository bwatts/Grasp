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
	public sealed class Validator : Notion, IValueCalculator
	{
		public static readonly Field<Identifier> OutputVariableIdentifierField = Field.On<Validator>.For(x => x.OutputVariableIdentifier);
		public static readonly Field<Rule> RuleField = Field.On<Validator>.For(x => x.Rule);

		public Validator(Identifier outputVariableIdentifier, Rule rule)
		{
			Contract.Requires(outputVariableIdentifier != null);
			Contract.Requires(rule != null);

			OutputVariableIdentifier = outputVariableIdentifier;
			Rule = rule;
		}

		public Validator(string outputVariableIdentifier, Rule rule) : this(new Identifier(outputVariableIdentifier), rule)
		{}

		public Identifier OutputVariableIdentifier { get { return GetValue(OutputVariableIdentifierField); } private set { SetValue(OutputVariableIdentifierField, value); } }
		public Rule Rule { get { return GetValue(RuleField); } private set { SetValue(RuleField, value); } }

		public Calculation GetCalculation(Variable target)
		{



			// TODO: Variables in the rule need to be qualified with the target variable's namespace



			return Calculation.FromRule(target, Rule, target.Name.ToNamespace() + OutputVariableIdentifier);
		}
	}
}