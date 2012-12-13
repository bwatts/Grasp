using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using Grasp.Checks.Rules;
using Grasp.Knowledge.Structure;

namespace Grasp.Knowledge.Forms
{
	[ContractClass(typeof(ValueInputContract))]
	public abstract class ValueInput : Input
	{
		public static readonly Field<Type> TypeField = Field.On<ValueInput>.For(x => x.Type);

		public static readonly Identifier HasValueIdentifier = new Identifier("HasValue");
		public static readonly Identifier ValidIdentifier = new Identifier("Valid");

		protected ValueInput(Type type, FullName name = null) : base(name)
		{
			Contract.Requires(type != null);

			Type = type;
		}

		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }

		public override Question GetQuestion()
		{
			var valueVariable = new Variable(Type, Namespace.Root.ToFullName());
			var hasValueVariable = new Variable<bool>(Namespace.Root + HasValueIdentifier);

			return new ValueQuestion(Type, GetCalculators(valueVariable, hasValueVariable), Name);
		}

		protected virtual IEnumerable<IValueCalculator> GetCalculators(Variable valueVariable, Variable hasValueVariable)
		{
			Contract.Requires(valueVariable != null);
			Contract.Requires(hasValueVariable != null);
			Contract.Ensures(Contract.Result<IEnumerable<IValueCalculator>>() != null);

			var hasValueValidator = new Validator(HasValueIdentifier, GetHasValueRule());

			var valueValidators = GetValidators(valueVariable, hasValueVariable).ToList();

			var validValidator = GetValidValidator(valueValidators);

			return Params.Of(validValidator, hasValueValidator).Concat(valueValidators);
		}

		protected abstract Rule GetHasValueRule();

		protected abstract IEnumerable<Validator> GetValidators(Variable valueVariable, Variable hasValueVariable);

		private static Validator GetValidValidator(IEnumerable<Validator> validators)
		{
			var valueValidRule = validators
				.Select(validator => new FullName(validator.OutputVariableIdentifier))
				.Select(validatorVariableName => new Variable<bool>(validatorVariableName).ToExpression())
				.Select(outputVariable => Rule.Result(outputVariable))
				.DefaultIfEmpty(Rule.True)
				.Aggregate<Rule>((leftRule, rightRule) => Rule.And(leftRule, rightRule));

			return new Validator(ValidIdentifier, valueValidRule);
		}
	}

	[ContractClassFor(typeof(ValueInput))]
	internal abstract class ValueInputContract : ValueInput
	{
		protected ValueInputContract(Type type) : base(type)
		{}

		protected override Rule GetHasValueRule()
		{
			Contract.Ensures(Contract.Result<Rule>() != null);

			return null;
		}

		protected override IEnumerable<Validator> GetValidators(Variable valueVariable, Variable hasValueVariable)
		{
			Contract.Requires(valueVariable != null);
			Contract.Requires(hasValueVariable != null);
			Contract.Ensures(Contract.Result<IEnumerable<Validator>>() != null);

			return null;
		}
	}
}