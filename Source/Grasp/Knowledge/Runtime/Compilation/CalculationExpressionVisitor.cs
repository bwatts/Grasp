using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	internal abstract class CalculationExpressionVisitor : ExpressionVisitor
	{
		public override Expression Visit(Expression node)
		{
			return node.NodeType == VariableExpression.ExpressionType ? VisitVariable((VariableExpression) node) : base.Visit(node);
		}

		protected virtual Expression VisitVariable(VariableExpression node)
		{
			return node;
		}
	}
}