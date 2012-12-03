using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Forms
{
	[ContractClass(typeof(ValueInputContract))]
	public abstract class ValueInput : Input
	{
		public static readonly Field<Type> TypeField = Field.On<ValueInput>.For(x => x.Type);
		public static readonly Field<bool> RequiredField = Field.On<ValueInput>.For(x => x.Required);

		protected ValueInput(Type type, bool required = false, FullName name = null) : base(name)
		{
			Contract.Requires(type != null);

			Type = type;
			Required = required;
		}

		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }
		public bool Required { get { return GetValue(RequiredField); } private set { SetValue(RequiredField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var valueVariable = new Variable(Type, rootNamespace + new Identifier("Value"));

			return new Schema(Params.Of(valueVariable), GetCalculations(rootNamespace, valueVariable));
		}

		private IEnumerable<Calculation> GetCalculations(Namespace rootNamespace, Variable valueVariable)
		{
			yield return new Calculation<bool>(rootNamespace + new Identifier("HasValue"), GetHasValueExpression(valueVariable));

			foreach(var calculation in GetOtherCalculations(rootNamespace, valueVariable))
			{
				yield return calculation;
			}
		}

		public abstract Expression GetHasValueExpression(Variable valueVariable);

		public abstract IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable);
	}

	[ContractClassFor(typeof(ValueInput))]
	internal abstract class ValueInputContract : ValueInput
	{
		protected ValueInputContract(Type type) : base(type)
		{}

		public override Expression GetHasValueExpression(Variable valueVariable)
		{
			Contract.Requires(valueVariable != null);
			Contract.Ensures(Contract.Result<Expression>() != null);
			Contract.Ensures(Contract.Result<Expression>().Type == typeof(bool));

			return null;
		}

		public override IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable)
		{
			Contract.Requires(rootNamespace != null);
			Contract.Requires(valueVariable != null);
			Contract.Ensures(Contract.Result<IEnumerable<Calculation>>() != null);

			return null;
		}
	}
}