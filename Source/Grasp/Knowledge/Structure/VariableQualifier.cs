using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Structure
{
	internal sealed class VariableQualifier : CalculationExpressionVisitor
	{
		private readonly Namespace _rootNamespace;

		internal VariableQualifier(Namespace rootNamespace)
		{
			_rootNamespace = rootNamespace;
		}

		protected override Expression VisitVariable(VariableExpression node)
		{
			var qualifiedVariable = QualifyVariable(node.Variable);

			return qualifiedVariable == node.Variable ? node : new VariableExpression(qualifiedVariable);
		}

		private Variable QualifyVariable(Variable variable)
		{
			return IsRootNamespace(variable.Name.Namespace) || IsRootNamespace(variable.Name.ToNamespace())
				? variable
				: new Variable(variable.Type, _rootNamespace + variable.Name);
		}

		private bool IsRootNamespace(Namespace @namespace)
		{
			return @namespace == _rootNamespace;
		}
	}
}