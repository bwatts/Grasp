using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	public class SubQuestion : Question
	{
		public static readonly Field<Identifier> VariableNameField = Field.On<SubQuestion>.For(x => x.VariableName);
		public static readonly Field<Question> QuestionField = Field.On<SubQuestion>.For(x => x.Question);

		public SubQuestion(Identifier variableName, Question question, FullName name = null) : base(name)
		{
			Contract.Requires(variableName != null);
			Contract.Requires(question != null);

			VariableName = variableName;
			Question = question;
		}

		public SubQuestion(string variableName, Question question, FullName name = null) : this(new Identifier(variableName), question, name)
		{}

		public Identifier VariableName { get { return GetValue(VariableNameField); } private set { SetValue(VariableNameField, value); } }
		public Question Question { get { return GetValue(QuestionField); } private set { SetValue(QuestionField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			return Question.GetSchema(new Namespace(rootNamespace + VariableName));
		}
	}
}