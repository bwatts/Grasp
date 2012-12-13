using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Structure
{
	public class ValueQuestion : Question
	{
		public static readonly Field<Type> TypeField = Field.On<ValueQuestion>.For(x => x.Type);
		public static readonly Field<Many<IValueCalculator>> CalculatorsField = Field.On<ValueQuestion>.For(x => x.Calculators);

		public ValueQuestion(Type type, IEnumerable<IValueCalculator> calculators = null, FullName name = null) : base(name)
		{
			Contract.Requires(type != null);

			Type = type;
			Calculators = (calculators ?? Enumerable.Empty<IValueCalculator>()).ToMany();
		}

		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }
		public Many<IValueCalculator> Calculators { get { return GetValue(CalculatorsField); } private set { SetValue(CalculatorsField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var variable = new Variable(Type, rootNamespace.ToFullName());

			return new Schema(Params.Of(variable), GetCalculations(variable));
		}

		private IEnumerable<Calculation> GetCalculations(Variable target)
		{
			return Calculators.Select(calculator => GetCalculation(target, calculator));
		}

		private static Calculation GetCalculation(Variable target, IValueCalculator calculator)
		{
			// Questions are generally created outside of a root namespace, via classes derived from Grasp.Knowledge.Forms.Input. This means all variables referenced
			// by calculations are relative to the root of the input. It isn't until a consumer calls GetSchema and provides a root namespace that we can qualify
			// those variables with the correct namespace.
			//
			// Calculations are always defined in the namespace of the target variable.

			return QualifyVariables(target, calculator.GetCalculation(target));
		}

		private static Calculation QualifyVariables(Variable target, Calculation calculation)
		{
			var transposedExpression = new VariableQualifier(target.Name.ToNamespace()).Visit(calculation.Expression);

			return transposedExpression == calculation.Expression
				? calculation
				: new Calculation(calculation.OutputVariable, transposedExpression);
		}
	}

	public class ValueQuestion<T> : ValueQuestion
	{
		public ValueQuestion(IEnumerable<IValueCalculator> calculators = null, FullName name = null) : base(typeof(T), calculators, name)
		{}
	}
}