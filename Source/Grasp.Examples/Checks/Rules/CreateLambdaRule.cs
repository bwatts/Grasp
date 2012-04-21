using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules
{
	public class CreateLambdaRule : Behavior
	{
		LambdaExpression _lambda;
		LambdaRule _lambdaRule;

		protected override void Given()
		{
			var parameter = Expression.Parameter(typeof(int), "target");

			_lambda = Expression.Lambda(Expression.Equal(parameter, Expression.Constant(0)), parameter);
		}

		protected override void When()
		{
			_lambdaRule = Rule.Lambda(_lambda);
		}

		[Then]
		public void HasLambdaType()
		{
			Assert.That(_lambdaRule.Type, Is.EqualTo(RuleType.Lambda));
		}

		[Then]
		public void HasOriginalLambda()
		{
			Assert.That(_lambdaRule.Lambda, Is.EqualTo(_lambda));
		}
	}
}