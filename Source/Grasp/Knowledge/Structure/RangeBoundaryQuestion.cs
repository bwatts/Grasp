using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	public class RangeBoundaryQuestion : Question
	{
		public static readonly Field<Identifier> VariableNameField = Field.On<RangeBoundaryQuestion>.For(x => x.VariableName);
		public static readonly Field<ValueQuestion> ValueField = Field.On<RangeBoundaryQuestion>.For(x => x.Value);

		public RangeBoundaryQuestion(Identifier variableName, ValueQuestion value, FullName name = null) : base(name)
		{
			Contract.Requires(variableName != null);
			Contract.Requires(value != null);

			VariableName = variableName;
			Value = value;
		}

		public Identifier VariableName { get { return GetValue(VariableNameField); } private set { SetValue(VariableNameField, value); } }
		public ValueQuestion Value { get { return GetValue(ValueField); } private set { SetValue(ValueField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			return Value.GetSchema(new Namespace(rootNamespace + VariableName));
		}
	}
}