using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Grasp.Knowledge.Runtime.Compilation;

namespace Grasp.Knowledge
{
	/// <summary>
	/// A context in which a set of variables and a set of calculations are in effect
	/// </summary>
	public class Schema : Notion
	{
		public static readonly Field<Many<Variable>> VariablesField = Field.On<Schema>.For(x => x.Variables);
		public static readonly Field<Many<Calculation>> CalculationsField = Field.On<Schema>.For(x => x.Calculations);

		/// <summary>
		/// Initializes a schema with the specified variables and calculations
		/// </summary>
		/// <param name="variables">The variables in effect for this schema</param>
		/// <param name="calculations">The calculations in effect for this schema</param>
		public Schema(IEnumerable<Variable> variables = null, IEnumerable<Calculation> calculations = null)
		{
			Variables = (variables ?? Enumerable.Empty<Variable>()).ToMany();
			Calculations = (calculations ?? Enumerable.Empty<Calculation>()).ToMany();
		}

		/// <summary>
		/// Gets the variables in effect for this schema
		/// </summary>
		public Many<Variable> Variables { get { return GetValue(VariablesField); } private set { SetValue(VariablesField, value); } }

		/// <summary>
		/// Gets the calculations in effect for this schema
		/// </summary>
		public Many<Calculation> Calculations { get { return GetValue(CalculationsField); } private set { SetValue(CalculationsField, value); } }

		/// <summary>
		/// Gets an executable version of this schema which applies its calculations
		/// </summary>
		/// <returns>An executable version of this schema which applies its calculations</returns>
		/// <exception cref="CompilationException">
		/// Throw if any calculation references a variable that is not defined in either <see cref="Variables"/> or as the output variable of another calculation
		/// -or- any calculation's result type is not assignable to its output variable type -or- any calculation contains a cycle -or- any calculation contains
		/// an invalid expression tree -or- there is any other error while compiling this schema
		/// </exception>
		public Executable Compile()
		{
			try
			{
				return new GraspCompiler(this).Compile();
			}
			catch(Exception ex)
			{
				throw new CompilationException(this, Resources.CompilationError, ex);
			}
		}
	}
}