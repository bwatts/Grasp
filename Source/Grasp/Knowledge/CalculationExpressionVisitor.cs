using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Grasp.Knowledge
{
	internal abstract class CalculationExpressionVisitor : ExpressionVisitor
	{
		protected override Expression VisitExtension(Expression node)
		{
			var variableNode = node as VariableExpression;

			return variableNode != null ? VisitVariable(variableNode) : base.Visit(variableNode);
		}

		protected virtual Expression VisitVariable(VariableExpression node)
		{
			return node;
		}
	}
}