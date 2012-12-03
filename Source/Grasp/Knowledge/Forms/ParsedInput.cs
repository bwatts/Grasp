using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Forms
{
	public class ParsedInput : ValueInput
	{
		public static readonly Field<TextInput> TextField = Field.On<ParsedInput>.For(x => x.Text);

		public ParsedInput(Type type, TextInput text, bool required = false, FullName name = null) : base(type, required, name)
		{
			Contract.Requires(text != null);

			Text = text;
		}

		public TextInput Text { get { return GetValue(TextField); } private set { SetValue(TextField, value); } }

		public override Expression GetHasValueExpression(Variable valueVariable)
		{
			return Text.GetHasValueExpression(valueVariable);
		}

		public override IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable)
		{
			// TODO: Calculation whose output is the parsed value

			// TODO: Calculation whose output is whether the parse succeeded

			return Text.GetOtherCalculations(rootNamespace, valueVariable);
		}
	}
}