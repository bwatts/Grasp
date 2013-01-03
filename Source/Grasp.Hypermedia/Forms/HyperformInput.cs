using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Knowledge;
using Grasp.Knowledge.Forms;

namespace Grasp.Hypermedia.Forms
{
	public class HyperformInput : NamedNotion
	{
		public static readonly Field<Input> InputField = Field.On<HyperformInput>.For(x => x.Input);
		public static readonly Field<string> LabelField = Field.On<HyperformInput>.For(x => x.Label);

		public HyperformInput(FullName name, Input input, string label = null) : base(name)
		{
			Contract.Requires(name != null);
			Contract.Requires(input != null);

			Input = input;
			Label = label ?? "";
		}

		public Input Input { get { return GetValue(InputField); } private set { SetValue(InputField, value); } }
		public string Label { get { return GetValue(LabelField); } private set { SetValue(LabelField, value); } }
	}
}