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
	public class ExclusiveOrRuleToLambda : Behavior
	{
		bool _leftValue;
		bool _rightValue;
		BinaryRule _exclusiveOrRule;
		Type _targetType;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_leftValue = false;
			_rightValue = true;

			_exclusiveOrRule = Rule.ExclusiveOr(Rule.Constant(_leftValue), Rule.Constant(_rightValue));

			_targetType = typeof(int);
		}

		protected override void When()
		{
			_lambda = _exclusiveOrRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => false ^ true

		[Then]
		public void HasOneParameter()
		{
			Assert.That(_lambda.Parameters.Count, Is.EqualTo(1));
		}

		[Then]
		public void ParameterIsTargetType()
		{
			var parameter = _lambda.Parameters.Single();

			Assert.That(parameter.Type, Is.EqualTo(_targetType));
		}

		[Then]
		public void ParameterNameIsTarget()
		{
			var parameter = _lambda.Parameters.Single();

			Assert.That(parameter.Name, Is.EqualTo("target"));
		}

		[Then]
		public void BodyIsBinary()
		{
			Assert.That(_lambda.Body, Is.InstanceOf<BinaryExpression>());
		}

		[Then]
		public void BodyNodeTypeIsExclusiveOr()
		{
			Assert.That(_lambda.Body.NodeType, Is.EqualTo(ExpressionType.ExclusiveOr));
		}

		[Then]
		public void ExlcusiveOrLeftIsExpectedType()
		{
			var exlcusiveOrExpression = (BinaryExpression) _lambda.Body;

			Assert.That(exlcusiveOrExpression.Left, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void ExlcusiveOrRightIsExpectedType()
		{
			var exlcusiveOrExpression = (BinaryExpression) _lambda.Body;

			Assert.That(exlcusiveOrExpression.Right, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void ExlcusiveOrLeftValueIsExpectedValue()
		{
			var exlcusiveOrExpression = (BinaryExpression) _lambda.Body;
			var left = (ConstantExpression) exlcusiveOrExpression.Left;

			Assert.That(left.Value, Is.EqualTo(_leftValue));
		}

		[Then]
		public void ExlcusiveOrRightValueIsExpectedValue()
		{
			var exlcusiveOrExpression = (BinaryExpression) _lambda.Body;
			var right = (ConstantExpression) exlcusiveOrExpression.Right;

			Assert.That(right.Value, Is.EqualTo(_rightValue));
		}
	}
}