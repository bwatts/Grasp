using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Knowledge.Structure;
using Grasp.Messaging;
using Grasp.Work;

namespace Grasp.Knowledge.Forms
{
	public class Form : Notion
	{
		public static readonly Field<FullName> NameField = Field.On<Form>.For(x => x.Name);
		public static readonly Field<ManyInOrder<Input>> InputsField = Field.On<Form>.For(x => x.Inputs);

		public Form(FullName name = null, IEnumerable<Input> inputs = null)
		{
			Name = name ?? FullName.Anonymous;
			Inputs = (inputs ?? Enumerable.Empty<Input>()).ToManyInOrder();
		}

		public FullName Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
		public ManyInOrder<Input> Inputs { get { return GetValue(InputsField); } private set { SetValue(InputsField, value); } }

		public IEnumerable<Question> GetQuestions()
		{
			return Inputs.Select(input => input.GetQuestion());
		}
	}
}