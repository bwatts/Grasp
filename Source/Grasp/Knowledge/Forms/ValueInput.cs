using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Forms
{
	[ContractClass(typeof(ValueInputContract))]
	public abstract class ValueInput : Input
	{
		public static readonly Field<Type> TypeField = Field.On<ValueInput>.For(x => x.Type);

		protected ValueInput(Type type, FullName name = null) : base(name)
		{
			Contract.Requires(type != null);

			Type = type;
		}

		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var valueVariable = new Variable(Type, rootNamespace + new Identifier("Value"));

			return new Schema(Params.Of(valueVariable), GetCalculations(rootNamespace, valueVariable));
		}

		private IEnumerable<Calculation> GetCalculations(Namespace rootNamespace, Variable valueVariable)
		{
			var hasValueCalculation = Calculation.FromRule(valueVariable, GetHasValueRule(valueVariable), rootNamespace + new Identifier("HasValue"));

			yield return hasValueCalculation;

			foreach(var calculation in GetOtherCalculations(rootNamespace, valueVariable, hasValueCalculation.OutputVariable))
			{
				yield return calculation;
			}
		}

		public abstract Rule GetHasValueRule(Variable valueVariable);

		public abstract IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable, Variable hasValueVariable);
	}

	[ContractClassFor(typeof(ValueInput))]
	internal abstract class ValueInputContract : ValueInput
	{
		protected ValueInputContract(Type type) : base(type)
		{}

		public override Rule GetHasValueRule(Variable valueVariable)
		{
			Contract.Requires(valueVariable != null);
			Contract.Ensures(Contract.Result<Rule>() != null);

			return null;
		}

		public override IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable, Variable hasValueVariable)
		{
			Contract.Requires(rootNamespace != null);
			Contract.Requires(valueVariable != null);
			Contract.Requires(hasValueVariable != null);
			Contract.Ensures(Contract.Result<IEnumerable<Calculation>>() != null);

			return null;
		}
	}
}