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
	public class ConstantRuleToLambda : Behavior
	{
		bool _value;
		ConstantRule _constantRule;
		Type _targetType;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_value = true;

			_constantRule = Rule.Constant(_value);

			_targetType = typeof(int);
		}

		protected override void When()
		{
			_lambda = _constantRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => true

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
		public void BodyIsConstant()
		{
			Assert.That(_lambda.Body, Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void ConstantValueIsOriginal()
		{
			var constant = (ConstantExpression) _lambda.Body;

			Assert.That(constant.Value, Is.EqualTo(_value));
		}
	}
}