using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Forms
{
	public class ParsedInput : ValueInput
	{
		public static readonly Field<TextInput> TextField = Field.On<ParsedInput>.For(x => x.Text);

		public ParsedInput(Type type, TextInput text, FullName name = null) : base(type, name)
		{
			Contract.Requires(text != null);

			Text = text;
		}

		public TextInput Text { get { return GetValue(TextField); } private set { SetValue(TextField, value); } }

		public override Rule GetHasValueRule(Variable valueVariable)
		{
			return Text.GetHasValueRule(valueVariable);
		}

		public override IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable, Variable hasValueVariable)
		{
			// TODO: Calculation whose output is the parsed value

			// TODO: Calculation whose output is whether the parse succeeded

			return Text.GetOtherCalculations(rootNamespace, valueVariable, hasValueVariable);
		}
	}
}