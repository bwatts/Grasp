using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp.Checks;
using Grasp.Checks.Methods;
using Grasp.Checks.Rules;

namespace Grasp.Knowledge.Structure
{
	public class RangeQuestion : Question
	{
		public static readonly Field<RangeBoundaryQuestion> MinimumField = Field.On<RangeQuestion>.For(x => x.Minimum);
		public static readonly Field<RangeBoundaryQuestion> MaximumField = Field.On<RangeQuestion>.For(x => x.Maximum);
		public static readonly Field<Identifier> ValidVariableNameField = Field.On<RangeQuestion>.For(x => x.ValidVariableName);

		public RangeQuestion(RangeBoundaryQuestion minimum, RangeBoundaryQuestion maximum, Identifier validVariableName, FullName name = null) : base(name)
		{
			Contract.Requires(minimum != null);
			Contract.Requires(maximum != null);
			Contract.Requires(minimum.Value.Type == maximum.Value.Type);
			Contract.Requires(validVariableName != null);

			Minimum = minimum;
			Maximum = maximum;
			ValidVariableName = validVariableName;
		}

		public RangeBoundaryQuestion Minimum { get { return GetValue(MinimumField); } private set { SetValue(MinimumField, value); } }
		public RangeBoundaryQuestion Maximum { get { return GetValue(MaximumField); } private set { SetValue(MaximumField, value); } }
		public Identifier ValidVariableName { get { return GetValue(ValidVariableNameField); } private set { SetValue(ValidVariableNameField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var minimumSchema = Minimum.GetSchema(rootNamespace);
			var maximumSchema = Maximum.GetSchema(rootNamespace);

			var minimum = minimumSchema.Variables.Single();
			var maximum = maximumSchema.Variables.Single();

			var calculations = GetCalculations(rootNamespace, minimum, maximum, minimumSchema.Calculations, maximumSchema.Calculations);

			return new Schema(Params.Of(minimum, maximum), calculations);
		}

		private IEnumerable<Calculation> GetCalculations(
			Namespace rootNamespace,
			Variable minimum,
			Variable maximumn,
			IEnumerable<Calculation> minimumCalculations,
			IEnumerable<Calculation> maximumCalculations)
		{
			var validCalculation = GetCalculation(rootNamespace, minimum, maximumn);

			return minimumCalculations.Concat(maximumCalculations).Concat(Params.Of(validCalculation));
		}

		private Calculation GetCalculation(Namespace rootNamespace, Variable minimum, Variable maximum)
		{
			return new Calculation<bool>(rootNamespace + ValidVariableName, GetValidExpression(minimum, maximum));
		}

		private static Expression GetValidExpression(Variable minimum, Variable maximum)
		{
			return Expression.LessThanOrEqual(minimum.ToExpression(), maximum.ToExpression());
		}
	}
}