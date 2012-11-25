using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	internal sealed class CalculationSchema
	{
		internal CalculationSchema(Calculation source)
		{
			Source = source;

			Variables = new VariableSearch().GetVariables(source);
		}

		internal Calculation Source { get; private set; }

		internal Expression Expression
		{
			get { return Source.Expression; }
		}

		internal Variable OutputVariable
		{
			get { return Source.OutputVariable; }
		}

		internal ISet<Variable> Variables { get; private set; }

		public override string ToString()
		{
			return Source.ToString();
		}
	}
}