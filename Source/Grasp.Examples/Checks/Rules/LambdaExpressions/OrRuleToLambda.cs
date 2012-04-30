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
	public class OrRuleToLambda : Behavior
	{
		bool _leftValue;
		bool _rightValue;
		BinaryRule _orRule;
		Type _targetType;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_leftValue = false;
			_rightValue = true;

			_orRule = Rule.Or(Rule.Constant(_leftValue), Rule.Constant(_rightValue));

			_targetType = typeof(int);
		}

		protected override void When()
		{
			_lambda = _orRule.ToLambdaExpression(_targetType);
		}

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
		public void BodyNodeTypeIsOrElse()
		{
			Assert.That(_lambda.Body.NodeType, Is.EqualTo(ExpressionType.OrElse));
		}

		[Then]
		public void OrLeftIsExpectedType()
		{
			var orExpression = (BinaryExpression) _lambda.Body;

			Assert.That(orExpression.Left, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void OrRightIsExpectedType()
		{
			var orExpression = (BinaryExpression) _lambda.Body;

			Assert.That(orExpression.Right, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void OrLeftValueIsExpectedValue()
		{
			var orExpression = (BinaryExpression) _lambda.Body;
			var left = (ConstantExpression) orExpression.Left;

			Assert.That(left.Value, Is.EqualTo(_leftValue));
		}

		[Then]
		public void OrRightValueIsExpectedValue()
		{
			var orExpression = (BinaryExpression) _lambda.Body;
			var right = (ConstantExpression) orExpression.Right;

			Assert.That(right.Value, Is.EqualTo(_rightValue));
		}
	}
}