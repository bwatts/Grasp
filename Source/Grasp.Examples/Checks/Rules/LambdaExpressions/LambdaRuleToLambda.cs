using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Checks.Rules.LambdaExpressions
{
	public class LambdaRuleToLambda : Behavior
	{
		LambdaExpression _lambda;
		LambdaRule _lambdaRule;
		Type _targetType;
		LambdaExpression _resultLambda;

		protected override void Given()
		{
			_targetType = typeof(int);

			_lambda = Expression.Lambda(Expression.Constant(true), Expression.Parameter(_targetType, "innerTarget"));

			_lambdaRule = Rule.Lambda(_lambda);
		}

		protected override void When()
		{
			_resultLambda = _lambdaRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => <inner lambda>(target)

		[Then]
		public void HasOneParameter()
		{
			Assert.That(_resultLambda.Parameters.Count, Is.EqualTo(1));
		}

		[Then]
		public void ParameterIsTargetType()
		{
			var parameter = _resultLambda.Parameters.Single();

			Assert.That(parameter.Type, Is.EqualTo(_targetType));
		}

		[Then]
		public void ParameterNameIsTarget()
		{
			var parameter = _resultLambda.Parameters.Single();

			Assert.That(parameter.Name, Is.EqualTo("target"));
		}

		[Then]
		public void BodyIsInvocation()
		{
			Assert.That(_resultLambda.Body, Is.InstanceOf<InvocationExpression>());
		}

		[Then]
		public void InvocationLambdaIsOriginal()
		{
			var invocationExpression = (InvocationExpression) _resultLambda.Body;

			Assert.That(invocationExpression.Expression, Is.EqualTo(_lambda));
		}

		[Then]
		public void InvocationHasOneArgument()
		{
			var invocationExpression = (InvocationExpression) _resultLambda.Body;

			Assert.That(invocationExpression.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void InvocationArgumentIsTargetParameter()
		{
			var parameter = _resultLambda.Parameters.Single();

			var invocationExpression = (InvocationExpression) _resultLambda.Body;
			var argument = invocationExpression.Arguments.Single();

			Assert.That(argument, Is.EqualTo(parameter));
		}
	}
}