using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Grasp.Compilation;

namespace Grasp
{
	public class GraspSchema
	{
		public GraspSchema(IEnumerable<Variable> variables, IEnumerable<Calculation> calculations)
		{
			Contract.Requires(variables != null);
			Contract.Requires(calculations != null);

			Variables = variables.ToList().AsReadOnly();
			Calculations = calculations.ToList().AsReadOnly();
		}

		public ReadOnlyCollection<Variable> Variables { get; private set; }

		public ReadOnlyCollection<Calculation> Calculations { get; private set; }

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