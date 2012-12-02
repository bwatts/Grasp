using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Grasp.Knowledge.Structure
{
	[ContractClass(typeof(QuestionContract))]
	public abstract class Question : Notion
	{
		public static readonly Field<FullName> NameField = Field.On<Question>.For(x => x.Name);

		protected Question(FullName name = null)
		{
			Name = name ?? FullName.Anonymous;
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public abstract Schema GetSchema(Namespace rootNamespace);

		public Schema GetSchema(string rootNamespace)
		{
			return GetSchema(new Namespace(rootNamespace));
		}
	}

	[ContractClassFor(typeof(Question))]
	internal abstract class QuestionContract : Question
	{
		public override Schema GetSchema(Namespace rootNamespace)
		{
			Contract.Requires(rootNamespace != null);
			Contract.Ensures(Contract.Result<Schema>() != null);

			return null;
		}
	}
}