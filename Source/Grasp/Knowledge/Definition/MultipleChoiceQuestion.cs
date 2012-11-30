using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition.Selections
{
	public class MultipleChoiceQuestion : ChoiceQuestion
	{
		public static readonly Field<ManyInOrder<MultipleChoiceItem>> ChoicesField = Field.On<MultipleChoiceQuestion>.For(x => x.Items);

		public MultipleChoiceQuestion(FullName name, Identifier selectionVariableName, IEnumerable<MultipleChoiceItem> items = null) : base(name, selectionVariableName)
		{
			Items = (items ?? Enumerable.Empty<MultipleChoiceItem>()).ToManyInOrder();
		}

		public MultipleChoiceQuestion(string name, Identifier selectionVariableName, IEnumerable<MultipleChoiceItem> items = null) : this(new FullName(name), selectionVariableName)
		{}

		public ManyInOrder<MultipleChoiceItem> Items { get { return GetValue(ChoicesField); } private set { SetValue(ChoicesField, value); } }

		protected override Schema GetSelectionSchema(Namespace selectionNamespace)
		{
			return Items
				.Select(item => item.GetSchema(selectionNamespace))
				.Aggregate((left, right) => left.Merge(right));
		}
	}
}