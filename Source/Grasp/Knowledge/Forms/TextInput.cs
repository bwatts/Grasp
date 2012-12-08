using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Forms
{
	public class TextInput : ValueInput
	{
		public static readonly Field<Count> MinimumLengthField = Field.On<TextInput>.For(x => x.MinimumLength);
		public static readonly Field<Count> MaximumLengthField = Field.On<TextInput>.For(x => x.MaximumLength);
		public static readonly Field<ITextPattern> PatternField = Field.On<TextInput>.For(x => x.Pattern);

		public static readonly Identifier LengthValidIdentifier = new Identifier("LengthValid");
		public static readonly Identifier PatternValidIdentifier = new Identifier("PatternValid");

		public TextInput(Count? minimumLength = null, Count? maximumLength = null, ITextPattern pattern = null, FullName name = null) : base(typeof(string), name)
		{
			MinimumLength = minimumLength ?? Count.None;
			MaximumLength = maximumLength ?? Count.None;
			Pattern = pattern;
		}

		public Count MinimumLength { get { return GetValue(MinimumLengthField); } private set { SetValue(MinimumLengthField, value); } }
		public Count MaximumLength { get { return GetValue(MaximumLengthField); } private set { SetValue(MaximumLengthField, value); } }
		public ITextPattern Pattern { get { return GetValue(PatternField); } private set { SetValue(PatternField, value); } }

		public override Rule GetHasValueRule(SchemaBuilder schema)
		{
			return new IsNotNullOrEmptyMethod().GetRule(typeof(string));
		}

		public override void DefineSchema(SchemaBuilder schema, Variable valueVariable, Variable hasValueVariable)
		{
			if(MinimumLength != Count.None || MaximumLength != Count.None)
			{
				DefineLengthSchema(schema, valueVariable, hasValueVariable);
			}

			if(Pattern != null)
			{
				DefinePatternSchema(schema, valueVariable, hasValueVariable);
			}
		}

		private void DefineLengthSchema(SchemaBuilder schema, Variable valueVariable, Variable hasValueVariable)
		{
			schema.Add(Calculation.FromRule(
				valueVariable,
				GetLengthRule(hasValueVariable),
				schema.GetRootedName(LengthValidIdentifier)));
		}

		private Rule GetLengthRule(Variable hasValueVariable)
		{
			var hasValueRule = Rule.Result(hasValueVariable.ToExpression());

			return Rule.And(hasValueRule, GetLengthRule());
		}

		private Rule GetLengthRule()
		{
			var lengthProperty = Reflect.Property((string x) => x.Length);

			var lengthValueRule = GetLengthValueCheck().GetRule(typeof(int));

			return Rule.Property(lengthProperty, lengthValueRule);
		}

		private ICheckMethod GetLengthValueCheck()
		{
			if(MinimumLength != Count.None && MaximumLength != Count.None)
			{
				return new IsBetweenMethod(MinimumLength.Value, MaximumLength.Value);
			}
			else if(MinimumLength != Count.None)
			{
				return new IsGreaterThanOrEqualToMethod(MinimumLength.Value);
			}
			else
			{
				return new IsLessThanOrEqualToMethod(MaximumLength.Value);
			}
		}

		private void DefinePatternSchema(SchemaBuilder schema, Variable valueVariable, Variable hasValueVariable)
		{
			var hasValueRule = Rule.Result(hasValueVariable.ToExpression());

			var patternRule = Rule.And(hasValueRule, Pattern.GetRule());

			schema.Add(Calculation.FromRule(
				valueVariable,
				patternRule,
				schema.GetRootedName(PatternValidIdentifier)));
		}
	}
}