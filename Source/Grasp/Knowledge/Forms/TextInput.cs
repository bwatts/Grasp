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

		public TextInput(Count? minimumLength = null, Count? maximumLength = null, ITextPattern pattern = null, FullName name = null) : base(typeof(string), name)
		{
			MinimumLength = minimumLength ?? Count.None;
			MaximumLength = maximumLength ?? Count.None;
			Pattern = pattern;
		}

		public Count MinimumLength { get { return GetValue(MinimumLengthField); } private set { SetValue(MinimumLengthField, value); } }
		public Count MaximumLength { get { return GetValue(MaximumLengthField); } private set { SetValue(MaximumLengthField, value); } }
		public ITextPattern Pattern { get { return GetValue(PatternField); } private set { SetValue(PatternField, value); } }

		public override Rule GetHasValueRule(Variable valueVariable)
		{
			return new IsNotNullOrEmptyMethod().GetRule(typeof(string));
		}

		public override IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable, Variable hasValueVariable)
		{
			if(MinimumLength != Count.None || MaximumLength != Count.None)
			{
				yield return GetLengthCalculation(rootNamespace, valueVariable, hasValueVariable);
			}

			if(Pattern != null)
			{
				yield return GetPatternCalculation(rootNamespace, valueVariable, hasValueVariable);
			}
		}

		private Calculation GetLengthCalculation(Namespace rootNamespace, Variable valueVariable, Variable hasValueVariable)
		{
			return Calculation.FromRule(valueVariable, GetLengthRule(hasValueVariable), rootNamespace + new Identifier("LengthValid"));
		}

		private Rule GetLengthRule(Variable hasValueVariable)
		{
			var hasValueRule = Rule.Result(hasValueVariable.ToExpression());

			var lengthProperty = Reflect.Property((string x) => x.Length);

			var lengthRule = Rule.Property(lengthProperty, GetLengthValueRule());

			return Rule.And(hasValueRule, lengthRule);
		}

		private Rule GetLengthValueRule()
		{
			if(MinimumLength != Count.None && MaximumLength != Count.None)
			{
				return new IsBetweenMethod(MinimumLength.Value, MaximumLength.Value).GetRule(typeof(int));
			}
			else if(MinimumLength != Count.None)
			{
				return new IsGreaterThanOrEqualToMethod(MinimumLength.Value).GetRule(typeof(int));
			}
			else
			{
				return new IsLessThanOrEqualToMethod(MaximumLength.Value).GetRule(typeof(int));
			}
		}

		private Calculation GetPatternCalculation(Namespace rootNamespace, Variable valueVariable, Variable hasValueVariable)
		{
			var hasValueRule = Rule.Result(hasValueVariable.ToExpression());

			var patternRule = Rule.And(hasValueRule, Pattern.GetRule());

			return Calculation.FromRule(valueVariable, patternRule, rootNamespace + new Identifier("PatternValid"));
		}
	}
}