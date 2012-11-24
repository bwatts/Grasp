using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
			_body = Expression.Constant(node.IsTrue);

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

			left = EnsureBooleanResult(left);

			var right = EnsureBooleanResult(_body);

			if(node.Type == RuleType.And)
			{
				_body = Expression.AndAlso(left, right);
			}
			else if(node.Type == RuleType.Or)
			{
				_body = Expression.OrElse(left, right);
			}
			else
			{
				_body = Expression.ExclusiveOr(left, right);
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
			var priorTarget = _target;

			_target = node.Type == RuleType.Method
				? Expression.Call(priorTarget, (MethodInfo) node.Member)
				: Expression.MakeMemberAccess(priorTarget, node.Member) as Expression;

			Visit(node.Rule);

			_target = priorTarget;

			return node;
		}

		private static Expression EnsureBooleanResult(Expression expression)
		{
			return expression.Type == typeof(bool) ? expression : Expression.Convert(expression, typeof(bool));
		}
	}
}