using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Forms
{
	[ContractClass(typeof(InputContract))]
	public abstract class Input : Notion
	{
		public static readonly Field<FullName> NameField = Field.On<Input>.For(x => x.Name);

		protected Input(FullName name = null)
		{
			Name = name ?? FullName.Anonymous;
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public virtual Schema GetSchema(Namespace rootNamespace = null)
		{
			var schema = new SchemaBuilder(rootNamespace ?? Namespace.Root);

			DefineSchema(schema);

			return schema;
		}

		protected abstract void DefineSchema(SchemaBuilder schema);
	}

	[ContractClassFor(typeof(Input))]
	internal abstract class InputContract : Input
	{
		protected override void DefineSchema(SchemaBuilder schema)
		{
			Contract.Requires(schema != null);
		}
	}
}