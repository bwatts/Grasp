using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Checks.Rules
{
	internal sealed class ConvertRuleToLambdaExpression : RuleVisitor
	{
		ParameterExpression _targetParameter;
		Expression _body;

		internal LambdaExpression ConvertToLambdaExpression(Rule rule, Type targetType)
		{
			_targetParameter = Expression.Parameter(targetType, "target");

			Visit(rule);

			return Expression.Lambda(_body, _targetParameter);
		}

		protected override Rule VisitCheck(CheckRule node)
		{
			return node;
		}

		protected override Rule VisitConstant(ConstantRule node)
		{
			_body = Expression.Constant(node.Passes);

			return node;
		}

		protected override Rule VisitLambda(LambdaRule node)
		{
			_body = Expression.Invoke(node.Lambda, _targetParameter);

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