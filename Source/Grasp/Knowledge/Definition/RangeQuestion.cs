using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Work.Items;

namespace Grasp.Knowledge.Definition
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

			var calculations = GetCalculations(minimumSchema, maximumSchema, GetValid(rootNamespace, minimum, maximum));

			return new Schema(Params.Of(minimum, maximum), calculations);
		}

		private Calculation GetValid(Namespace rootNamespace, Variable minimum, Variable maximum)
		{
			return new Calculation<bool>(
				rootNamespace + ValidationRule.Namespace + ValidVariableName,
				Expression.LessThanOrEqual(minimum.ToExpression(), maximum.ToExpression()));
		}

		private static IEnumerable<Calculation> GetCalculations(Schema minimumSchema, Schema maximumSchema, Calculation valid)
		{
			return minimumSchema.Calculations.Concat(maximumSchema.Calculations).Concat(Params.Of(valid));
		}
	}
}