using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.Linq;

namespace Grasp.Checks.Rules
{
	internal sealed class ConvertRuleToLambdaExpression : RuleVisitor
	{
		private Expression _target;
		private Expression _body;

		internal LambdaExpression ConvertToLambdaExpression(Rule rule, Type targetType)
		{
			var targetParameter = Expression.Parameter(targetType, "target");

			_target = targetParameter;

			Visit(rule);

			return Expression.Lambda(EnsureBooleanResult(_body), targetParameter);
		}

		protected override Rule VisitCheck(CheckRule node)
		{
			var baseCheck = Expression.Call(typeof(Check), "That", new[] { _target.Type }, _target);

			var arguments = new[] { baseCheck }.Concat(node.CheckArguments.ToConstants());

			_body = Expression.Call(node.Method, arguments);

			return node;
		}

		protected override Rule VisitConstant(ConstantRule node)
		{
			_body = Expression.Constant(node.Passes);

			return node;
		}

		protected override Rule VisitLambda(LambdaRule node)
		{
			_body = Expression.Invoke(node.Lambda, _target);

			return node;
		}

		protected override Rule VisitBinary(BinaryRule node)
		{
			Visit(node.Left);

			var left = _body;

			Visit(node.Right);

			if(node.Type == RuleType.And)
			{
				_body = Expression.AndAlso(EnsureBooleanResult(left), EnsureBooleanResult(_body));
			}
			else if(node.Type == RuleType.Or)
			{
				_body = Expression.OrElse(EnsureBooleanResult(left), EnsureBooleanResult(_body));
			}
			else
			{
				_body = Expression.ExclusiveOr(EnsureBooleanResult(left), EnsureBooleanResult(_body));
			}

			return node;
		}

		protected override Rule VisitNot(NotRule node)
		{
			Visit(node.Rule);

			_body = Expression.Not(EnsureBooleanResult(_body));

			return node;
		}

		protected override Rule VisitMember(MemberRule node)
		{
			return node;
		}

		private static Expression EnsureBooleanResult(Expression expression)
		{
			return expression.Type == typeof(bool) ? expression : Expression.Convert(expression, typeof(bool));
		}
	}
}