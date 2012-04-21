using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Grasp.Compilation;

namespace Grasp
{
	/// <summary>
	/// A context in which a set of variables and a set of calculations are in effect
	/// </summary>
	public class GraspSchema
	{
		/// <summary>
		/// Initializes a schema with the specified variables and calculations
		/// </summary>
		/// <param name="variables">The variables in effect for this schema</param>
		/// <param name="calculations">The calculations in effect for this schema</param>
		public GraspSchema(IEnumerable<Variable> variables, IEnumerable<Calculation> calculations)
		{
			Contract.Requires(variables != null);
			Contract.Requires(calculations != null);

			Variables = variables.ToList().AsReadOnly();
			Calculations = calculations.ToList().AsReadOnly();
		}

		/// <summary>
		/// Gets the variables in effect for this schema
		/// </summary>
		public ReadOnlyCollection<Variable> Variables { get; private set; }

		/// <summary>
		/// Gets the calculations in effect for this schema
		/// </summary>
		public ReadOnlyCollection<Calculation> Calculations { get; private set; }

		/// <summary>
		/// Gets an executable version of this schema which applies its calculations
		/// </summary>
		/// <returns>An executable version of this schema which applies its calculations</returns>
		/// <exception cref="CompilationException">
		/// Throw if any calculation references a variable that is not defined in either <see cref="Variables"/> or as the output variable of another calculation
		/// -or- any calculation's result type is not assignable to its output variable type -or- any calculation contains a cycle -or- any calculation contains
		/// an invalid expression tree -or- there is any other error while compiling this schema</exception>
		public GraspExecutable Compile()
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