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
	public class Form : NamedNotion
	{
		public static readonly Field<ManyInOrder<Input>> InputsField = Field.On<Form>.For(x => x.Inputs);

		public Form(FullName name = null, IEnumerable<Input> inputs = null) : base(name)
		{
			Inputs = (inputs ?? Enumerable.Empty<Input>()).ToManyInOrder();
		}

		public ManyInOrder<Input> Inputs { get { return GetValue(InputsField); } private set { SetValue(InputsField, value); } }

		public IEnumerable<Question> GetQuestions()
		{
			return Inputs.Select(input => input.GetQuestion());
		}
	}
}