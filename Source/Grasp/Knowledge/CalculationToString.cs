using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cloak;
using Cloak.Reflection;
using Grasp.Checks;
using Grasp.Knowledge.Forms;

namespace Grasp.Knowledge
{
	internal class CalculationToString : CalculationExpressionVisitor
	{
		private StringBuilder _text;

		internal string GetText(Calculation calculation)
		{
			_text = new StringBuilder();

			Visit(calculation.Expression);

			return Resources.Calculation.FormatInvariant(calculation.OutputVariable, _text);
		}

		public override Expression Visit(Expression node)
		{
			if(node == null)
			{
				return null;
			}
			else if(ShouldRewrite(node))
			{
				return base.Visit(node);
			}
			else
			{
				_text.Append(node);

				return node;
			}
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			if(node.Type == typeof(string) || node.Type == typeof(Regex))	// Hacky to include anything but string here
			{
				_text.Append('"').Append(node.Value).Append('"');
			}
			else
			{
				_text.Append(node.Value);
			}

			return node;
		}

		protected override Expression VisitConditional(ConditionalExpression node)
		{
			var testText = GetInnerText(() => Visit(node.Test));
			var ifTrueText = GetInnerText(() => Visit(node.IfTrue));
			var ifFalseText = GetInnerText(() => Visit(node.IfFalse));

			if(testText.Length + ifTrueText.Length + ifFalseText.Length <= 72)
			{
				_text.Append("(").Append(testText).Append(" ? ").Append(ifTrueText).Append(" : ").Append(ifFalseText).Append(")");
			}
			else
			{
				_text.Append("(").AppendLine(testText)
					.Append("\t? ").AppendLine(ifTrueText)
					.Append("\t: ").Append(ifFalseText).Append(")");
			}

			return node;
		}

		protected override Expression VisitInvocation(InvocationExpression node)
		{
			// (target: SomeInt.Value) => target.Success

			if(node.Expression.NodeType != ExpressionType.Lambda)
			{
				_text.Append(node);
			}
			else
			{
				var lambda = (LambdaExpression) node.Expression;

				_text.Append("(");

				for(var i = 0; i < lambda.Parameters.Count; i++)
				{
					if(i > 0)
					{
						_text.Append(", ");
					}

					_text.Append(lambda.Parameters[i].Name).Append(": ");

					Visit(node.Arguments[i]);
				}

				_text.Append(")");

				_text.Append(" => ");

				Visit(lambda.Body);
			}

			return node;
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if(node.Object == null
				&& node.Method.Name == "That"
				&& node.Method.DeclaringType == typeof(Check))
			{
				_text.Append("Check.That(");

				Visit(node.Arguments[0]);

				var defaultResultConstant = node.Arguments[1] as ConstantExpression;

				if(defaultResultConstant == null || !((bool) defaultResultConstant.Value))
				{
					_text.Append(", defaultResult: ");

					Visit(node.Arguments[1]);
				}

				_text.Append(")");
			}
			else if(node.Method.IsExtension())
			{
				Visit(node.Arguments[0]);

				_text.Append(".").Append(node.Method.Name).Append("(");

				VisitSeparated(", ", node.Arguments.Skip(1));

				_text.Append(")");
			}
			else
			{
				if(node.Object == null)
				{
					_text.Append(node.Type.Name);
				}
				else
				{
					Visit(node.Object);
				}

				_text.Append(".").Append(node.Method.Name).Append("(");

				VisitSeparated(", ", node.Arguments);

				_text.Append(")");
			}

			return node;
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			switch(node.NodeType)
			{
				case ExpressionType.Not:
					_text.Append("!");

					Visit(node.Operand);
					break;
				case ExpressionType.Convert:
					if(IsImplicitBooleanConversion(node))
					{
						Visit(node.Operand);
					}
					else
					{
						_text.Append(node.Operand);
					}
					break;
				default:
					_text.Append(node);
					break;
			}

			return node;
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			switch(node.NodeType)
			{
				case ExpressionType.And:
					VisitBinaryOperator(node, "&");
					break;
				case ExpressionType.AndAlso:
					VisitBinaryOperator(node, "&&");
					break;
				case ExpressionType.Or:
					VisitBinaryOperator(node, "|");
					break;
				case ExpressionType.OrElse:
					VisitBinaryOperator(node, "||");
					break;
				case ExpressionType.ExclusiveOr:
					VisitBinaryOperator(node, "^");
					break;
				default:
					_text.Append(node);
					break;
			}

			return node;
		}

		private void VisitBinaryOperator(BinaryExpression node, string token)
		{
			Visit(node.Left);

			_text.Append(" ").Append(token).Append(" ");

			Visit(node.Right);
		}

		private static bool ShouldRewrite(Expression node)
		{
			return node is UnaryExpression
				|| node is BinaryExpression
				|| Check.That(node.NodeType).IsIn(ExpressionType.Constant, ExpressionType.Conditional, ExpressionType.Invoke, ExpressionType.Call);
		}

		private string GetInnerText(Action visit)
		{
			var priorText = _text;

			_text = new StringBuilder();

			visit();

			var innerText = _text.ToString();

			_text = priorText;

			return innerText;
		}

		private void VisitSeparated(string separator, IEnumerable<Expression> expressions)
		{
			var wroteFirst = false;

			foreach(var expression in expressions)
			{
				if(wroteFirst)
				{
					_text.Append(separator);
				}
				else
				{
					wroteFirst = true;
				}

				Visit(expression);
			}
		}

		private static bool IsImplicitBooleanConversion(UnaryExpression node)
		{
			return node.Type == typeof(bool) && node.Operand.Type.IsAssignableToGenericDefinition(typeof(Check<>));
		}
	}
}