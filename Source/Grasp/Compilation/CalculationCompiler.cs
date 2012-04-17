using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Grasp.Compilation
{
	internal sealed class CalculationCompiler : CalculationExpressionVisitor
	{
		private static readonly MethodInfo _getVariableValueMethod = typeof(GraspRuntime).GetMethod("GetVariableValue", BindingFlags.Public | BindingFlags.Instance);
		private ParameterExpression _runtimeParameter;

		internal FunctionCalculator CompileCalculation(CalculationSchema schema)
		{
			_runtimeParameter = Expression.Parameter(typeof(GraspRuntime), "runtime");

			var lambdaBody = Visit(schema.Expression);

			return new FunctionCalculator(schema.OutputVariable, CompileFunction(lambdaBody));
		}

		protected override Expression VisitVariable(VariableExpression node)
		{
			return Expression.Convert(GetGetVariableValueCall(node), node.Variable.Type);
		}

		private Func<GraspRuntime, object> CompileFunction(Expression lambdaBody)
		{
			if(lambdaBody.Type != typeof(object))
			{
				lambdaBody = Expression.Convert(lambdaBody, typeof(object));
			}

			var lambda = Expression.Lambda<Func<GraspRuntime, object>>(lambdaBody, _runtimeParameter);

			return lambda.Compile();
		}

		private Expression GetGetVariableValueCall(VariableExpression variableNode)
		{
			return Expression.Call(_runtimeParameter, _getVariableValueMethod, Expression.Constant(variableNode.Variable));
		}
	}
}