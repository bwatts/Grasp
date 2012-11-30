using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Definition
{
	public class SingleChoiceQuestion : ChoiceQuestion
	{
		public static readonly Field<ManyInOrder<Choice>> ChoicesField = Field.On<SingleChoiceQuestion>.For(x => x.Choices);

		public SingleChoiceQuestion(FullName name, Identifier selectionVariableName, IEnumerable<Choice> choices = null) : base(name, selectionVariableName)
		{
			Choices = (choices ?? Enumerable.Empty<Choice>()).ToManyInOrder();
		}

		public SingleChoiceQuestion(string name, Identifier selectionVariableName, IEnumerable<Choice> choices = null) : this(new FullName(name), selectionVariableName)
		{}

		public ManyInOrder<Choice> Choices { get { return GetValue(ChoicesField); } private set { SetValue(ChoicesField, value); } }

		protected override Schema GetSelectionSchema(Namespace selectionNamespace)
		{
			var otherChoiceSchemas = Choices
				.Where(choice => choice.HasOtherQuestion)
				.Select(choice => choice.OtherQuestion.GetSchema(selectionNamespace))
				.ToList();

			return otherChoiceSchemas.Any() ? GetOtherSchema(otherChoiceSchemas) : GetScalarSchema(selectionNamespace);
		}

		private static Schema GetOtherSchema(IEnumerable<Schema> otherChoiceSchemas)
		{
			return otherChoiceSchemas.Aggregate((left, right) => left.Merge(right));
		}

		private static Schema GetScalarSchema(Namespace selectionNamespace)
		{
			var scalarVariable = new Variable<string>(new FullName(selectionNamespace));

			return new Schema(Params.Of(scalarVariable));
		}
	}
}