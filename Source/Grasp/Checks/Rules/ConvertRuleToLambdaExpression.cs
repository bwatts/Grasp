using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grasp.Checks.Rules
{
	internal sealed class ConvertRuleToLambdaExpression : RuleVisitor
	{
		internal LambdaExpression ConvertToLambdaExpression(Rule rule, Type targetType)
		{
			throw new NotImplementedException();
		}
	}
}