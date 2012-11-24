using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Analysis
{
	/// <summary>
	/// A function-based calculation which applies to <see cref="GraspRuntime"/>s
	/// </summary>
	public sealed class FunctionCalculator : Notion, ICalculator
	{
		public static readonly Field<Variable> OutputVariableField = Field.On<FunctionCalculator>.For(x => x.OutputVariable);
		public static readonly Field<Func<GraspRuntime, object>> FunctionField = Field.On<FunctionCalculator>.For(x => x.Function);

		/// <summary>
		/// Initializes a function calculator with the specified output variable and function
		/// </summary>
		/// <param name="outputVariable">The variable to which the output of the specified function is assigned</param>
		/// <param name="function">The function which calculates the value to be assigned to the specified output variable</param>
		public FunctionCalculator(Variable outputVariable, Func<GraspRuntime, object> function)
		{
			Contract.Requires(outputVariable != null);
			Contract.Requires(function != null);

			OutputVariable = outputVariable;
			Function = function;
		}

		/// <summary>
		/// Gets the variable to which the output of <see cref="Function"/> is assigned
		/// </summary>
		public Variable OutputVariable { get { return GetValue(OutputVariableField); } private set { SetValue(OutputVariableField, value); } }

		/// <summary>
		/// Gets the function which calculates the value to be assigned to <see cref="OutputVariable"/>
		/// </summary>
		public Func<GraspRuntime, object> Function { get { return GetValue(FunctionField); } private set { SetValue(FunctionField, value); } }

		/// <summary>
		/// Applies the function to the specified runtime
		/// </summary>
		/// <param name="runtime">A runtime to which the function is applied</param>
		public void ApplyCalculation(GraspRuntime runtime)
		{
			runtime.SetVariableValue(OutputVariable, Function(runtime));
		}
	}
}