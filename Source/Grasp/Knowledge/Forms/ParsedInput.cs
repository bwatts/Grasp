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
		public static readonly Field<IParseAlgorithm> AlgorithmField = Field.On<ParsedInput>.For(x => x.Algorithm);

		public static readonly Identifier TextIdentifier = new Identifier("Text");

		public ParsedInput(Type type, TextInput text, IParseAlgorithm algorithm, FullName name = null)
			: base(typeof(ParseResult<>).MakeGenericType(type), name)
		{
			Contract.Requires(text != null);
			Contract.Requires(algorithm != null);

			Text = text;
			Algorithm = algorithm;
		}

		public TextInput Text { get { return GetValue(TextField); } private set { SetValue(TextField, value); } }
		public IParseAlgorithm Algorithm { get { return GetValue(AlgorithmField); } private set { SetValue(AlgorithmField, value); } }

		public override Rule GetHasValueRule(SchemaBuilder schema, Variable valueVariable)
		{
			return Rule.Result(Expression.Property(valueVariable.ToExpression(), "Success"));
		}

		public override void DefineSchema(SchemaBuilder schema, Variable valueVariable, Variable hasValueVariable)
		{
			var x = GetX(schema, valueVariable, hasValueVariable);

			schema.Add(x.TextSchema);

			DefineSchema(x);
		}

		private X GetX(SchemaBuilder schema, Variable valueVariable, Variable hasValueVariable)
		{
			var textNamespace = schema.GetRootedName(TextIdentifier).ToNamespace();

			var textSchema = Text.GetSchema(textNamespace);

			var textVariable = textSchema.GetVariable(textNamespace + TextInput.ValueIdentifier);
			var hasTextVariable = textSchema.GetVariable(textNamespace + TextInput.HasValueIdentifier);

			return new X
			{
				Schema = schema,
				TextSchema = textSchema,
				TextVariable = textVariable,
				HasTextVariable = hasTextVariable,
				ValueVariable = valueVariable,
				HasValueVariable = hasValueVariable
			};
		}

		private void DefineSchema(X x)
		{
			// Value = Text.HasValue ? Algorithm.ParseValue<>(textVariable) : new ParseValue<>()

			x.Schema.Add(new Calculation(
				x.ValueVariable,
				Expression.Condition(
					x.HasTextVariable.ToExpression(),
					Expression.Call(Expression.Constant(Algorithm), "ParseValue", new[] { Type }, x.TextVariable.ToExpression()),
					Expression.New(typeof(ParseResult<>).MakeGenericType(Type)))));
		}

		private sealed class X
		{
			internal SchemaBuilder Schema;

			internal Schema TextSchema;
			internal Variable TextVariable;
			internal Variable HasTextVariable;

			internal Variable ValueVariable;
			internal Variable HasValueVariable;
		}
	}
}