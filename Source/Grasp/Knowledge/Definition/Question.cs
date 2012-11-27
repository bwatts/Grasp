using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Grasp.Knowledge.Definition
{
	[ContractClass(typeof(QuestionContract))]
	public abstract class Question : Notion
	{
		public static readonly Field<FullName> NameField = Field.On<Question>.For(x => x.Name);

		protected Question(FullName name)
		{
			Contract.Requires(name != null);

			Name = name;
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }

		public abstract Schema GetSchema(Namespace rootNamespace);
	}

	[ContractClassFor(typeof(Question))]
	internal abstract class QuestionContract : Question
	{
		internal QuestionContract() : base(null)
		{}

		public override Schema GetSchema(Namespace rootNamespace)
		{
			Contract.Requires(rootNamespace != null);
			Contract.Ensures(Contract.Result<Schema>() != null);

			return null;
		}
	}
}