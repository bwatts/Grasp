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
	public class NotRuleToLambda : Behavior
	{
		bool _value;
		ConstantRule _negatedRule;
		NotRule _notRule;
		Type _targetType;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_value = true;

			_negatedRule = Rule.Constant(_value);

			_notRule = Rule.Not(_negatedRule);

			_targetType = typeof(int);
		}

		protected override void When()
		{
			_lambda = _notRule.ToLambdaExpression(_targetType);
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
		public void BodyIsUnary()
		{
			Assert.That(_lambda.Body, Is.InstanceOf<UnaryExpression>());
		}

		[Then]
		public void BodyNodeTypeIsNot()
		{
			Assert.That(_lambda.Body.NodeType, Is.EqualTo(ExpressionType.Not));
		}

		[Then]
		public void NotOperandIsConstant()
		{
			var notExpression = (UnaryExpression) _lambda.Body;

			Assert.That(notExpression.Operand, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void NotOperandValueIsOriginal()
		{
			var notExpression = (UnaryExpression) _lambda.Body;
			var operand = (ConstantExpression) notExpression.Operand;

			Assert.That(operand.Value, Is.EqualTo(_value));
		}
	}
}