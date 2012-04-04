using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Compilation
{
	internal sealed class VariableSearch : CalculationExpressionVisitor
	{
		private ISet<Variable> _variables;

		internal ISet<Variable> GetVariables(Calculation calculation)
		{
			_variables = new HashSet<Variable>();

			Visit(calculation.Expression);

			return _variables;
		}

		protected override Expression VisitVariable(VariableExpression node)
		{
			_variables.Add(node.Variable);

			return node;
		}
	}
}