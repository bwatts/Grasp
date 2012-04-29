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
			return node;
		}

		protected override Rule VisitBinary(BinaryRule node)
		{
			return node;
		}

		protected override Rule VisitNot(NotRule node)
		{
			return node;
		}

		protected override Rule VisitMember(MemberRule node)
		{
			return node;
		}
	}
}