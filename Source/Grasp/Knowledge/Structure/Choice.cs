using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	public sealed class Choice : ComparableValue<Choice, string>
	{
		public static readonly Field<SubQuestion> SubQuestionField = Field.On<Choice>.For(x => x.SubQuestion);

		public Choice(string value, SubQuestion subQuestion = null) : base(value)
		{
			SubQuestion = subQuestion;
		}

		public SubQuestion SubQuestion { get { return GetValue(SubQuestionField); } private set { SetValue(SubQuestionField, value); } }

		public bool HasSubQuestion
		{
			get { return SubQuestion != null; }
		}

		public Schema GetSchema(Namespace rootNamespace)
		{
			Contract.Requires(rootNamespace != null);

			return HasSubQuestion ? SubQuestion.GetSchema(rootNamespace) : Schema.Empty;
		}
	}
}