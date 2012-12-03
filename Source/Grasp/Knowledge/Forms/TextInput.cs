using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Forms
{
	public class TextInput : ValueInput
	{
		public static readonly Field<Count> MinimumLengthField = Field.On<TextInput>.For(x => x.MinimumLength);
		public static readonly Field<Count> MaximumLengthField = Field.On<TextInput>.For(x => x.MaximumLength);
		public static readonly Field<TextPattern> PatternField = Field.On<TextInput>.For(x => x.Pattern);

		public TextInput(Count? minimumLength = null, Count? maximumLength = null, TextPattern pattern = null, bool required = false, FullName name = null)
			: base(typeof(string), required, name)
		{
			MinimumLength = minimumLength ?? Count.None;
			MaximumLength = maximumLength ?? Count.None;
			Pattern = pattern ?? TextPattern.All;
		}

		public Count MinimumLength { get { return GetValue(MinimumLengthField); } private set { SetValue(MinimumLengthField, value); } }
		public Count MaximumLength { get { return GetValue(MaximumLengthField); } private set { SetValue(MaximumLengthField, value); } }
		public TextPattern Pattern { get { return GetValue(PatternField); } private set { SetValue(PatternField, value); } }

		public override Expression GetHasValueExpression(Variable valueVariable)
		{
			// TODO: Check if not null or empty

			return Expression.Constant(true);
		}

		public override IEnumerable<Calculation> GetOtherCalculations(Namespace rootNamespace, Variable valueVariable)
		{
			// TODO: Minimum length calculation

			// TODO: Maximum length calculation

			// TODO: Pattern calculation

			yield break;
		}
	}
}