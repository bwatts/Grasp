﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Cloak.NUnit;
using Cloak.Reflection;
using NUnit.Framework;

namespace Grasp.Checks.Rules.LambdaExpressions
{
	public class CheckRuleToLambda : Behavior
	{
		MethodInfo _method;
		CheckRule _checkRule;
		Type _targetType;
		MethodInfo _thatMethod;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_method = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);

			_checkRule = Rule.Check(_method);

			_targetType = typeof(int);

			_thatMethod = typeof(Check)
				.GetMethods()
				.Where(method => method.Name == "That" && method.GetParameters().Count() == 1)
				.Single()
				.MakeGenericMethod(_targetType);
		}

		protected override void When()
		{
			_lambda = _checkRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => (bool) Check.That(target).IsPositive()

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
		public void BodyNodeTypeIsConvert()
		{
			Assert.That(_lambda.Body.NodeType, Is.EqualTo(ExpressionType.Convert));
		}

		[Then]
		public void BodyConvertsToBoolean()
		{
			var convert = (UnaryExpression) _lambda.Body;

			Assert.That(convert.Type, Is.EqualTo(typeof(bool)));
		}

		[Then]
		public void ConvertOperandIsMethodCall()
		{
			var convert = (UnaryExpression) _lambda.Body;

			Assert.That(convert.Operand, Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void ConvertOperandMethodCallIsCheckCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Method, Is.EqualTo(_method));
		}

		[Then]
		public void CheckCallHasOneArgument()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void CheckCallArgumentIsMethodCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void CheckCallArgumentIsThatCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();

			Assert.That(thatCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void ThatCallHasOneArgument()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void ThatCallArgumentIsTargetParameter()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(_lambda.Parameters.Single()));
		}
	}
}