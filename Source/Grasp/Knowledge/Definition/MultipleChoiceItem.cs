using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	public class MultipleChoiceItem : Notion
	{
		public static readonly Field<Identifier> VariableNameField = Field.On<MultipleChoiceItem>.For(x => x.VariableName);
		public static readonly Field<Choice> ChoiceField = Field.On<MultipleChoiceItem>.For(x => x.Choice);

		public MultipleChoiceItem(Identifier variableName, Choice choice)
		{
			Contract.Requires(variableName != null);
			Contract.Requires(choice != null);

			VariableName = variableName;
			Choice = choice;
		}

		public Identifier VariableName { get { return GetValue(VariableNameField); } private set { SetValue(VariableNameField, value); } }
		public Choice Choice { get { return GetValue(ChoiceField); } private set { SetValue(ChoiceField, value); } }

		public Schema GetSchema(Namespace selectionNamespace)
		{
			return Choice.GetSchema(new Namespace(selectionNamespace + VariableName));
		}
	}
}