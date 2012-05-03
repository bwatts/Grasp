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
	public class AndRuleToLambda : Behavior
	{
		bool _leftValue;
		bool _rightValue;
		BinaryRule _andRule;
		Type _targetType;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_leftValue = false;
			_rightValue = true;

			_andRule = Rule.And(Rule.Constant(_leftValue), Rule.Constant(_rightValue));

			_targetType = typeof(int);
		}

		protected override void When()
		{
			_lambda = _andRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => false && true

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
		public void BodyNodeTypeIsAndAlso()
		{
			Assert.That(_lambda.Body.NodeType, Is.EqualTo(ExpressionType.AndAlso));
		}

		[Then]
		public void AndLeftIsExpectedType()
		{
			var andExpression = (BinaryExpression) _lambda.Body;

			Assert.That(andExpression.Left, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void AndRightIsExpectedType()
		{
			var andExpression = (BinaryExpression) _lambda.Body;

			Assert.That(andExpression.Right, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void AndLeftValueIsExpectedValue()
		{
			var andExpression = (BinaryExpression) _lambda.Body;
			var left = (ConstantExpression) andExpression.Left;

			Assert.That(left.Value, Is.EqualTo(_leftValue));
		}

		[Then]
		public void AndRightValueIsExpectedValue()
		{
			var andExpression = (BinaryExpression) _lambda.Body;
			var right = (ConstantExpression) andExpression.Right;

			Assert.That(right.Value, Is.EqualTo(_rightValue));
		}
	}
}