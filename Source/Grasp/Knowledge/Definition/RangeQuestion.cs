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
		public static readonly Field<ValueQuestion> MinimumField = Field.On<RangeQuestion>.For(x => x.Minimum);
		public static readonly Field<ValueQuestion> MaximumField = Field.On<RangeQuestion>.For(x => x.Maximum);
		public static readonly Field<Identifier> ValidVariableNameField = Field.On<RangeQuestion>.For(x => x.ValidVariableName);

		public RangeQuestion(FullName name, ValueQuestion minimum, ValueQuestion maximum, Identifier validVariableName) : base(name)
		{
			Contract.Requires(minimum != null);
			Contract.Requires(maximum != null);
			Contract.Requires(validVariableName != null);

			Minimum = minimum;
			Maximum = maximum;
			ValidVariableName = validVariableName;
		}

		public RangeQuestion(string name, ValueQuestion minimum, ValueQuestion maximum, Identifier validVariableName)
			: this(new FullName(name), minimum, maximum, validVariableName)
		{}

		public ValueQuestion Minimum { get { return GetValue(MinimumField); } private set { SetValue(MinimumField, value); } }
		public ValueQuestion Maximum { get { return GetValue(MaximumField); } private set { SetValue(MaximumField, value); } }
		public Identifier ValidVariableName { get { return GetValue(ValidVariableNameField); } private set { SetValue(ValidVariableNameField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var minimumSchema = Minimum.GetSchema(rootNamespace);
			var maximumSchema = Maximum.GetSchema(rootNamespace);

			var minimum = minimumSchema.Variables.Single();
			var maximum = maximumSchema.Variables.Single();

			var valid = GetValid(rootNamespace, minimum, maximum);

			var calculations = minimumSchema.Calculations
				.Concat(maximumSchema.Calculations)
				.Concat(new[] { valid });

			return new Schema(Params.Of(minimum, maximum), calculations);
		}

		private Calculation GetValid(Namespace rootNamespace, Variable minimum, Variable maximum)
		{
			return new Calculation<bool>(
				rootNamespace + ValidationRule.Namespace + ValidVariableName,
				Expression.LessThanOrEqual(minimum.ToExpression(), maximum.ToExpression()));
		}
	}
}