using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Grasp.Knowledge.Structure
{
	[ContractClass(typeof(QuestionContract))]
	public abstract class Question : NamedNotion
	{
		protected Question(FullName name = null) : base(name)
		{}

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