using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Definition
{
	public sealed class Choice : ComparableValue<Choice, string>
	{
		public static readonly Field<SubQuestion> OtherQuestionField = Field.On<Choice>.For(x => x.OtherQuestion);

		public Choice(string value, SubQuestion otherQuestion = null) : base(value)
		{
			OtherQuestion = otherQuestion;
		}

		public SubQuestion OtherQuestion { get { return GetValue(OtherQuestionField); } private set { SetValue(OtherQuestionField, value); } }

		public bool HasOtherQuestion
		{
			get { return OtherQuestion != null; }
		}

		public Schema GetSchema(Namespace rootNamespace)
		{
			Contract.Requires(rootNamespace != null);

			return !HasOtherQuestion ? new Schema() : OtherQuestion.GetSchema(rootNamespace);
		}
	}
}