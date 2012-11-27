using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge.Definition
{
	public class ScalarQuestion : Question
	{
		public static readonly Field<Type> VariableTypeField = Field.On<ScalarQuestion>.For(x => x.VariableType);
		public static readonly Field<Identifier> VariableNameField = Field.On<ScalarQuestion>.For(x => x.VariableName);

		public ScalarQuestion(FullName name, Type variableType, Identifier variableName) : base(name)
		{
			Contract.Requires(variableType != null);
			Contract.Requires(variableName != null);

			VariableType = variableType;
			VariableName = variableName;
		}

		public Type VariableType { get { return GetValue(VariableTypeField); } private set { SetValue(VariableTypeField, value); } }
		public Identifier VariableName { get { return GetValue(VariableNameField); } private set { SetValue(VariableNameField, value); } }

		public override Schema GetSchema(Namespace rootNamespace)
		{
			var variable = new Variable(VariableType, rootNamespace + VariableName);

			return new Schema(variables: Params.Of(variable));
		}
	}
}