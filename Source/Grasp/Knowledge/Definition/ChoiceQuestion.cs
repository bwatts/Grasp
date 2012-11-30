using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	[ContractClass(typeof(ChoiceQuestionContract))]
	public abstract class ChoiceQuestion : Question
	{
		public static readonly Field<Identifier> SelectionVariableNameField = Field.On<ChoiceQuestion>.For(x => x.SelectionVariableName);
		
		protected ChoiceQuestion(FullName name, Identifier selectionVariableName) : base(name)
		{
			Contract.Requires(selectionVariableName != null);

			SelectionVariableName = selectionVariableName;
		}

		public Identifier SelectionVariableName { get { return GetValue(SelectionVariableNameField); } private set { SetValue(SelectionVariableNameField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var selectionNamespace = new Namespace(rootNamespace + SelectionVariableName);

			return GetSelectionSchema(selectionNamespace);
		}

		protected abstract Schema GetSelectionSchema(Namespace selectionNamespace);
	}

	[ContractClassFor(typeof(ChoiceQuestion))]
	internal abstract class ChoiceQuestionContract : ChoiceQuestion
	{
		protected ChoiceQuestionContract(FullName name, Identifier selectionVariableName) : base(name, selectionVariableName)
		{}

		protected override Schema GetSelectionSchema(Namespace selectionNamespace)
		{
			Contract.Requires(selectionNamespace != null);
			Contract.Ensures(Contract.Result<Schema>() != null);

			return null;
		}
	}
}