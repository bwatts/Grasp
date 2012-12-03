using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	[ContractClass(typeof(ChoiceQuestionContract))]
	public abstract class ChoiceQuestion : Question
	{
		public static readonly Field<ManyInOrder<Choice>> ChoicesField = Field.On<ChoiceQuestion>.For(x => x.Choices);

		protected ChoiceQuestion(IEnumerable<Choice> choices = null, FullName name = null) : base(name)
		{
			Choices = (choices ?? Enumerable.Empty<Choice>()).ToManyInOrder();
		}

		public ManyInOrder<Choice> Choices { get { return GetValue(ChoicesField); } private set { SetValue(ChoicesField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			return GetSelectionSchema(rootNamespace).Merge(GetChoicesSchema(rootNamespace));
		}

		private Schema GetSelectionSchema(Namespace rootNamespace)
		{
			var selectionVariableName = new FullName(rootNamespace);

			return new Schema(GetSelectionVariable(selectionVariableName));
		}

		private Schema GetChoicesSchema(Namespace rootNamespace)
		{
			return Choices.Select(choice => choice.GetSchema(rootNamespace)).Merge();
		}

		protected abstract Variable GetSelectionVariable(FullName name);
	}

	[ContractClassFor(typeof(ChoiceQuestion))]
	internal abstract class ChoiceQuestionContract : ChoiceQuestion
	{
		protected override Variable GetSelectionVariable(FullName name)
		{
			Contract.Requires(name != null);
			Contract.Ensures(Contract.Result<Variable>() != null);

			return null;
		}
	}
}