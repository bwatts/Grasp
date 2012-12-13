using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks.Rules;
using Grasp.Knowledge.Structure;

namespace Grasp.Knowledge.Forms
{
	public class ParsedInput : ValueInput
	{
		public static readonly Field<TextInput> TextField = Field.On<ParsedInput>.For(x => x.Text);
		public static readonly Field<Type> ParsedTypeField = Field.On<ParsedInput>.For(x => x.ParsedType);

		public static readonly Identifier TextQualifier = new Identifier("Text");

		public ParsedInput(TextInput text, Type parsedType, FullName name = null) : base(typeof(ConversionResult<>).MakeGenericType(parsedType), name)
		{
			Contract.Requires(text != null);
			Contract.Requires(parsedType != null);

			Text = text;
			ParsedType = parsedType;
		}

		public TextInput Text { get { return GetValue(TextField); } private set { SetValue(TextField, value); } }
		public Type ParsedType { get { return GetValue(ParsedTypeField); } private set { SetValue(ParsedTypeField, value); } }

		public override Question GetQuestion()
		{
			var textQuestion = (ValueQuestion) Text.GetQuestion();

			var valueVariable = new Variable(Type, Namespace.Root.ToFullName());
			var hasValueVariable = new Variable<bool>(Namespace.Root + HasValueIdentifier);

			var calculators = GetCalculators(valueVariable, hasValueVariable);

			return new ConversionQuestion(TextQualifier, textQuestion, TextInput.ValidIdentifier, new ParseConversion(ParsedType), calculators, Name);
		}

		protected override Rule GetHasValueRule()
		{
			return Rule.Property(Type.GetProperty("Success"), Rule.Literal);
		}

		protected override IEnumerable<Validator> GetValidators(Variable valueVariable, Variable hasValueVariable)
		{
			// TODO: Allow validators against the parsed value?
			//
			// Also, allow calculations against the parsed value by overriding GetCalculators?

			yield break;
		}
	}
}