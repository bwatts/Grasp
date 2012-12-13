using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Knowledge.Structure;

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

		public abstract Question GetQuestion();
	}

	[ContractClassFor(typeof(Input))]
	internal abstract class InputContract : Input
	{
		public override Question GetQuestion()
		{
			Contract.Ensures(Contract.Result<Question>() != null);

			return null;
		}
	}
}