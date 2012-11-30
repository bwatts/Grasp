using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	public class GridQuestion : Question
	{
		public static readonly Field<ManyInOrder<Question>> QuestionsField = Field.On<GridQuestion>.For(x => x.Questions);
		public static readonly Field<ManyInOrder<Identifier>> ItemVariableNamesField = Field.On<GridQuestion>.For(x => x.ItemVariableNames);

		public GridQuestion(FullName name, IEnumerable<Question> questions, IEnumerable<Identifier> itemVariableNames) : base(name)
		{
			Contract.Requires(questions != null);
			Contract.Requires(itemVariableNames != null);

			Questions = questions.ToManyInOrder();
			ItemVariableNames = itemVariableNames.ToManyInOrder();
		}

		public GridQuestion(string name, IEnumerable<Question> questions, IEnumerable<Identifier> itemVariableNames)
			: this(new FullName(name), questions, itemVariableNames)
		{}

		public ManyInOrder<Question> Questions { get { return GetValue(QuestionsField); } private set { SetValue(QuestionsField, value); } }
		public ManyInOrder<Identifier> ItemVariableNames { get { return GetValue(ItemVariableNamesField); } private set { SetValue(ItemVariableNamesField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var variables = new List<Variable>();
			var calculations = new List<Calculation>();

			var itemSchemas =
				from itemVariableName in ItemVariableNames
				from question in Questions
				let itemNamespace = new Namespace(rootNamespace + itemVariableName)
				select question.GetSchema(itemNamespace);

			foreach(var itemSchema in itemSchemas)
			{
				variables.AddRange(itemSchema.Variables);
				calculations.AddRange(itemSchema.Calculations);
			}

			return new Schema(variables, calculations);
		}
	}
}