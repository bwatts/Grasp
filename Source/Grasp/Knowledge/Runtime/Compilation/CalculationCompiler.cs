using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Grasp.Knowledge.Runtime.Compilation
{
	internal sealed class CalculationCompiler : CalculationExpressionVisitor
	{
		private static readonly MethodInfo _getVariableValueMethod = typeof(SchemaBinding).GetMethod("GetVariableValue", BindingFlags.Public | BindingFlags.Instance);
		private ParameterExpression _bindingParameter;

		internal FunctionCalculator CompileCalculation(CalculationSchema schema)
		{
			_bindingParameter = Expression.Parameter(typeof(SchemaBinding), "binding");

			var lambdaBody = Visit(schema.Expression);

			return new FunctionCalculator(schema.OutputVariable, CompileFunction(lambdaBody));
		}

		protected override Expression VisitVariable(VariableExpression node)
		{
			return Expression.Convert(GetGetVariableValueCall(node), node.Variable.Type);
		}

		private Func<SchemaBinding, object> CompileFunction(Expression lambdaBody)
		{
			if(lambdaBody.Type != typeof(object))
			{
				lambdaBody = Expression.Convert(lambdaBody, typeof(object));
			}

			var lambda = Expression.Lambda<Func<SchemaBinding, object>>(lambdaBody, _bindingParameter);

			return lambda.Compile();
		}

		private Expression GetGetVariableValueCall(VariableExpression variableNode)
		{
			return Expression.Call(_bindingParameter, _getVariableValueMethod, Expression.Constant(variableNode.Variable.Name));
		}
	}
}