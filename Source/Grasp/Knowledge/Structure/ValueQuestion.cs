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
			return Calculators.Select(calculator => calculator.GetCalculation(target));
		}
	}

	public class ValueQuestion<T> : ValueQuestion
	{
		public ValueQuestion(IEnumerable<IValueCalculator> calculators = null, FullName name = null) : base(typeof(T), calculators, name)
		{}
	}
}