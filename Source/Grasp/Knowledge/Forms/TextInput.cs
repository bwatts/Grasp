using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;
using Grasp.Knowledge.Structure;

namespace Grasp.Knowledge.Forms
{
	public class TextInput : ValueInput
	{
		public static readonly Field<bool> RequiredField = Field.On<TextInput>.For(x => x.Required);
		public static readonly Field<Count> MinimumLengthField = Field.On<TextInput>.For(x => x.MinimumLength);
		public static readonly Field<Count> MaximumLengthField = Field.On<TextInput>.For(x => x.MaximumLength);
		public static readonly Field<ITextPattern> PatternField = Field.On<TextInput>.For(x => x.Pattern);

		public static readonly Identifier RequiredValidIdentifier = new Identifier("RequiredValid");
		public static readonly Identifier LengthValidIdentifier = new Identifier("LengthValid");
		public static readonly Identifier PatternValidIdentifier = new Identifier("PatternValid");

		public TextInput(bool required = false, Count? minimumLength = null, Count? maximumLength = null, ITextPattern pattern = null, FullName name = null)
			: base(typeof(string), name)
		{
			Required = required;
			MinimumLength = minimumLength ?? Count.None;
			MaximumLength = maximumLength ?? Count.None;
			Pattern = pattern;
		}

		public bool Required { get { return GetValue(RequiredField); } private set { SetValue(RequiredField, value); } }
		public Count MinimumLength { get { return GetValue(MinimumLengthField); } private set { SetValue(MinimumLengthField, value); } }
		public Count MaximumLength { get { return GetValue(MaximumLengthField); } private set { SetValue(MaximumLengthField, value); } }
		public ITextPattern Pattern { get { return GetValue(PatternField); } private set { SetValue(PatternField, value); } }

		protected override Rule GetHasValueRule()
		{
			return new IsNotNullOrEmptyMethod().GetRule(typeof(string));
		}

		protected override IEnumerable<Validator> GetValidators(Variable valueVariable, Variable hasValueVariable)
		{
			if(Required)
			{
				yield return new Validator(RequiredValidIdentifier, Rule.Result(hasValueVariable.ToExpression()));
			}

			if(MinimumLength != Count.None || MaximumLength != Count.None)
			{
				yield return new Validator(LengthValidIdentifier, GetLengthRule(hasValueVariable));
			}

			if(Pattern != null)
			{
				yield return new Validator(PatternValidIdentifier, GetPatternRule(hasValueVariable));
			}
		}

		private Rule GetLengthRule(Variable hasValueVariable)
		{
			var hasValueRule = Rule.Result(hasValueVariable.ToExpression());

			var lengthRule = GetLengthRule();

			// When not required, it is valid to not have a value
			//
			// Not required:	!hasValue || lengthValid
			// Required:			hasValue && lengthValid

			return Required
				? Rule.And(hasValueRule, lengthRule)
				: Rule.Or(Rule.Not(hasValueRule), lengthRule);
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

		private Rule GetPatternRule(Variable hasValueVariable)
		{
			var hasValueRule = Rule.Result(hasValueVariable.ToExpression());

			var patternRule = Pattern.GetRule();

			// When not required, it is valid to not have a value
			//
			// Not required:	!hasValue || patternValid
			// Required:			hasValue && patternValid

			return Required
				? Rule.And(hasValueRule, patternRule)
				: Rule.Or(Rule.Not(hasValueRule), patternRule);
		}
	}
}