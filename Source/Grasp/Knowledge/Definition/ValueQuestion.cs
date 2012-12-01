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
		public static readonly Field<Many<IValidationRule>> ValidationRulesField = Field.On<ValueQuestion>.For(x => x.ValidationRules);

		public ValueQuestion(Type variableType, IEnumerable<IValidationRule> validationRules = null, FullName name = null) : base(name)
		{
			Contract.Requires(variableType != null);

			VariableType = variableType;
			ValidationRules = (validationRules ?? Enumerable.Empty<IValidationRule>()).ToMany();
		}

		public Type VariableType { get { return GetValue(VariableTypeField); } private set { SetValue(VariableTypeField, value); } }
		public Many<IValidationRule> ValidationRules { get { return GetValue(ValidationRulesField); } private set { SetValue(ValidationRulesField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var variable = new Variable(VariableType, new FullName(rootNamespace));

			return new Schema(Params.Of(variable), GetValidationRuleCalculations(variable));
		}

		private IEnumerable<Calculation<bool>> GetValidationRuleCalculations(Variable target)
		{
			return ValidationRules.Select(rule => rule.GetCalculation(target));
		}
	}

	public class ValueQuestion<T> : ValueQuestion
	{
		public ValueQuestion(IEnumerable<IValidationRule> validationRules = null, FullName name = null)
			: base(typeof(T), validationRules, name)
		{}
	}
}