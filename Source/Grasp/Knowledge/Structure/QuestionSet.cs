using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	public class QuestionSet : Notion
	{
		public static readonly Field<FullName> NameField = Field.On<QuestionSet>.For(x => x.Name);
		public static readonly Field<Many<Question>> QuestionsField = Field.On<QuestionSet>.For(x => x.Questions);

		public QuestionSet(FullName name, IEnumerable<Question> questions = null)
		{
			Contract.Requires(name != null);

			Name = name;
			Questions = (questions ?? Enumerable.Empty<Question>()).ToMany();
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public Many<Question> Questions { get { return GetValue(QuestionsField); } private set { SetValue(QuestionsField, value); } }
	}
}