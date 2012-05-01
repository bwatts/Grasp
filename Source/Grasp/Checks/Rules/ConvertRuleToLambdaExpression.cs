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
		ParameterExpression _target;
		Expression _body;

		internal LambdaExpression ConvertToLambdaExpression(Rule rule, Type targetType)
		{
			_target = Expression.Parameter(targetType, "target");

			Visit(rule);

			return Expression.Lambda(_body, _target);
		}

		protected override Rule VisitCheck(CheckRule node)
		{
			var thatCall = Expression.Call(typeof(Check), "That", new[] { _target.Type }, _target);

			var arguments = new[] { thatCall }.Concat(node.CheckArguments.ToConstants());

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
				_body = Expression.AndAlso(left, _body);
			}
			else if(node.Type == RuleType.Or)
			{
				_body = Expression.OrElse(left, _body);
			}
			else
			{
				_body = Expression.ExclusiveOr(left, _body);
			}

			return node;
		}

		protected override Rule VisitNot(NotRule node)
		{
			Visit(node.Rule);

			_body = Expression.Not(_body);

			return node;
		}

		protected override Rule VisitMember(MemberRule node)
		{
			return node;
		}
	}
}