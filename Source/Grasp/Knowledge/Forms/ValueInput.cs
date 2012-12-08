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

		public static readonly Identifier ValueIdentifier = new Identifier("Value");
		public static readonly Identifier HasValueIdentifier = new Identifier("HasValue");

		protected ValueInput(Type type, FullName name = null) : base(name)
		{
			Contract.Requires(type != null);

			Type = type;
		}

		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }

		protected override void DefineSchema(SchemaBuilder schema)
		{
			var valueVariable = schema.Add(Type, ValueIdentifier);

			DefineSchema(schema, valueVariable);
		}

		protected virtual void DefineSchema(SchemaBuilder schema, Variable valueVariable)
		{
			Contract.Requires(schema != null);
			Contract.Requires(valueVariable != null);

			var hasValueCalculation = Calculation.FromRule(valueVariable, GetHasValueRule(schema), schema.GetRootedName(HasValueIdentifier));

			schema.Add(hasValueCalculation);

			DefineSchema(schema, valueVariable, hasValueCalculation.OutputVariable);
		}

		public abstract Rule GetHasValueRule(SchemaBuilder schema);

		public abstract void DefineSchema(SchemaBuilder schema, Variable valueVariable, Variable hasValueVariable);
	}

	[ContractClassFor(typeof(ValueInput))]
	internal abstract class ValueInputContract : ValueInput
	{
		protected ValueInputContract(Type type) : base(type)
		{}

		public override Rule GetHasValueRule(SchemaBuilder schema)
		{
			Contract.Requires(schema != null);
			Contract.Ensures(Contract.Result<Rule>() != null);

			return null;
		}

		public override void DefineSchema(SchemaBuilder schema, Variable valueVariable, Variable hasValueVariable)
		{
			Contract.Requires(schema != null);
			Contract.Requires(valueVariable != null);
			Contract.Requires(hasValueVariable != null);
		}
	}
}