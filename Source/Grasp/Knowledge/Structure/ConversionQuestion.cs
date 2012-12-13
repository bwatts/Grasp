using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	public class ConversionQuestion : ValueQuestion
	{
		public static readonly Field<ValueQuestion> SourceQuestionField = Field.On<ConversionQuestion>.For(x => x.SourceQuestion);
		public static readonly Field<Identifier> SourceQualifierField = Field.On<ConversionQuestion>.For(x => x.SourceQualifier);
		public static readonly Field<Identifier> SourceValidIdentifierField = Field.On<ConversionQuestion>.For(x => x.SourceValidIdentifier);
		public static readonly Field<IConversion> ConversionField = Field.On<ConversionQuestion>.For(x => x.Conversion);

		public ConversionQuestion(
			Identifier sourceQualifier,
			ValueQuestion sourceQuestion,
			Identifier sourceValidIdentifier,
			IConversion conversion,
			IEnumerable<IValueCalculator> calculators = null,
			FullName name = null)
			: base(typeof(ConversionResult<>).MakeGenericType(conversion.ConvertedType), calculators, name)
		{
			Contract.Requires(sourceQualifier != null);
			Contract.Requires(sourceQuestion != null);
			Contract.Requires(sourceValidIdentifier != null);
			Contract.Requires(conversion != null);

			SourceQualifier = sourceQualifier;
			SourceQuestion = sourceQuestion;
			SourceValidIdentifier = sourceValidIdentifier;
			Conversion = conversion;
		}

		public Identifier SourceQualifier { get { return GetValue(SourceQualifierField); } private set { SetValue(SourceQualifierField, value); } }
		public ValueQuestion SourceQuestion { get { return GetValue(SourceQuestionField); } private set { SetValue(SourceQuestionField, value); } }
		public Identifier SourceValidIdentifier { get { return GetValue(SourceValidIdentifierField); } private set { SetValue(SourceValidIdentifierField, value); } }
		public IConversion Conversion { get { return GetValue(ConversionField); } private set { SetValue(ConversionField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var sourceName = rootNamespace + SourceQualifier;
			var sourceValidName = sourceName.ToNamespace() + SourceValidIdentifier;

			var sourceSchema = SourceQuestion.GetSchema(sourceName.ToNamespace());

			var sourceVariable = sourceSchema.GetVariable(sourceName);
			var sourceValidVariable = sourceSchema.GetVariable(sourceValidName);

			return sourceSchema.Merge(GetConversionSchema(rootNamespace, sourceVariable, sourceValidVariable));
		}

		private Schema GetConversionSchema(Namespace rootNamespace, Variable sourceVariable, Variable sourceValidVariable)
		{
			return new Schema(new Calculation(
				new Variable(Conversion.ConvertedType, rootNamespace.ToFullName()),
				GetConversionExpression(sourceVariable, sourceValidVariable)));
		}

		private Expression GetConversionExpression(Variable sourceVariable, Variable sourceValidVariable)
		{
			// Source.Valid ? (ConversionResult<>) Conversion.Convert(sourceVariable) : new ConversionResult<>()

			return Expression.Condition(
				sourceValidVariable.ToExpression(),
				Expression.Convert(
					Expression.Call(Expression.Constant(Conversion), "Convert", null, sourceVariable.ToExpression()),
					Type),
				Expression.New(Type));
		}
	}
}