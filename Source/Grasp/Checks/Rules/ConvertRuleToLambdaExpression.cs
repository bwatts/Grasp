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
		private bool _buildingCheck;

		internal LambdaExpression ConvertToLambdaExpression(Rule rule, Type targetType)
		{
			var targetParameter = Expression.Parameter(targetType, "target");

			_target = targetParameter;

			_buildingCheck = false;

			Visit(rule);

			return Expression.Lambda(_body, targetParameter);
		}

		protected override Rule VisitCheck(CheckRule node)
		{
			var baseCheck = _buildingCheck ? _body : Expression.Call(typeof(Check), "That", new[] { _target.Type }, _target);

			var arguments = new[] { baseCheck }.Concat(node.CheckArguments.ToConstants());

			_body = Expression.Call(node.Method, arguments);

			_buildingCheck = true;

			return node;
		}

		protected override Rule VisitConstant(ConstantRule node)
		{
			_body = Expression.Constant(node.Passes);

			_buildingCheck = false;

			return node;
		}

		protected override Rule VisitLambda(LambdaRule node)
		{
			_body = Expression.Invoke(node.Lambda, _target);

			_buildingCheck = false;

			return node;
		}

		protected override Rule VisitBinary(BinaryRule node)
		{
			Visit(node.Left);

			var left = _body;

			_buildingCheck = _buildingCheck && node.Type == RuleType.And;

			Visit(node.Right);

			if(node.Type == RuleType.And)
			{
				if(!_buildingCheck)
				{
					_body = Expression.AndAlso(left, _body);
				}
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

			_buildingCheck = false;

			return node;
		}

		protected override Rule VisitMember(MemberRule node)
		{
			_buildingCheck = false;

			return node;
		}
	}
}