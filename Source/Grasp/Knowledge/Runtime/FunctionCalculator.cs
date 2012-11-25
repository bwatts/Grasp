using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Runtime
{
	/// <summary>
	/// A function-based calculation which applies to bound schemas
	/// </summary>
	public sealed class FunctionCalculator : Notion, ICalculator
	{
		public static readonly Field<Variable> _outputVariableField = Field.On<FunctionCalculator>.For(x => x._outputVariable);
		public static readonly Field<Func<SchemaBinding, object>> _functionField = Field.On<FunctionCalculator>.For(x => x._function);

		private Variable _outputVariable { get { return GetValue(_outputVariableField); } set { SetValue(_outputVariableField, value); } }
		private Func<SchemaBinding, object> _function { get { return GetValue(_functionField); } set { SetValue(_functionField, value); } }

		/// <summary>
		/// Initializes a function calculator with the specified output variable and function
		/// </summary>
		/// <param name="outputVariable">The variable to which the output of the specified function is assigned</param>
		/// <param name="function">The function which calculates the value to be assigned to the specified output variable</param>
		public FunctionCalculator(Variable outputVariable, Func<SchemaBinding, object> function)
		{
			Contract.Requires(outputVariable != null);
			Contract.Requires(function != null);

			_outputVariable = outputVariable;
			_function = function;
		}

		/// <summary>
		/// Applies the function to the specified schema binding
		/// </summary>
		/// <param name="schemaBinding">The bound schema to which to apply the function</param>
		public void ApplyCalculation(SchemaBinding schemaBinding)
		{
			schemaBinding.SetVariableValue(_outputVariable, _function(schemaBinding));
		}
	}
}