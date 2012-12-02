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
		public static readonly Field<Many<IValidator>> ValidatorsField = Field.On<ValueQuestion>.For(x => x.Validators);

		public ValueQuestion(Type variableType, IEnumerable<IValidator> validators = null, FullName name = null) : base(name)
		{
			Contract.Requires(variableType != null);

			VariableType = variableType;
			Validators = (validators ?? Enumerable.Empty<IValidator>()).ToMany();
		}

		public Type VariableType { get { return GetValue(VariableTypeField); } private set { SetValue(VariableTypeField, value); } }
		public Many<IValidator> Validators { get { return GetValue(ValidatorsField); } private set { SetValue(ValidatorsField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var variable = new Variable(VariableType, new FullName(rootNamespace));

			return new Schema(Params.Of(variable), GetValidationCalculations(variable));
		}

		private IEnumerable<Calculation<bool>> GetValidationCalculations(Variable target)
		{
			return Validators.Select(validator => validator.GetRule(target).GetCalculation());
		}
	}

	public class ValueQuestion<T> : ValueQuestion
	{
		public ValueQuestion(IEnumerable<IValidator> validators = null, FullName name = null) : base(typeof(T), validators, name)
		{}
	}
}