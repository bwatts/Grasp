using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Definition
{
	public class ValueQuestion : Question
	{
		public static readonly Field<Type> VariableTypeField = Field.On<ValueQuestion>.For(x => x.VariableType);
		public static readonly Field<Identifier> VariableNameField = Field.On<ValueQuestion>.For(x => x.VariableName);
		public static readonly Field<Many<IValidationRule>> ValidationRulesField = Field.On<ValueQuestion>.For(x => x.ValidationRules);

		public ValueQuestion(FullName name, Type variableType, Identifier variableName, IEnumerable<IValidationRule> validationRules = null) : base(name)
		{
			Contract.Requires(variableType != null);
			Contract.Requires(variableName != null);

			VariableType = variableType;
			VariableName = variableName;
			ValidationRules = (validationRules ?? Enumerable.Empty<IValidationRule>()).ToMany();
		}

		public ValueQuestion(string name, Type variableType, Identifier variableName, IEnumerable<IValidationRule> validationRules = null)
			: this(new FullName(name), variableType, variableName, validationRules)
		{}

		public Type VariableType { get { return GetValue(VariableTypeField); } private set { SetValue(VariableTypeField, value); } }
		public Identifier VariableName { get { return GetValue(VariableNameField); } private set { SetValue(VariableNameField, value); } }
		public Many<IValidationRule> ValidationRules { get { return GetValue(ValidationRulesField); } private set { SetValue(ValidationRulesField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var variable = new Variable(VariableType, rootNamespace + VariableName);

			return new Schema(Params.Of(variable), GetValidationRuleCalculations(variable));
		}

		private IEnumerable<Calculation<bool>> GetValidationRuleCalculations(Variable target)
		{
			return ValidationRules.Select(rule => rule.GetCalculation(target));
		}
	}
}