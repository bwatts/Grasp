using System;
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
	public class CheckRuleWithArgumentToLambda : Behavior
	{
		string _value;
		MethodInfo _method;
		CheckRule _checkRule;
		Type _targetType;
		MethodInfo _thatMethod;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_value = "value";

			_method = Reflect.Func<ICheckable<string>, string, Check<string>>(Checkable.StartsWith);

			_checkRule = Rule.Check(_method, _value);

			_targetType = typeof(string);

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
		public void BodyIsMethodCall()
		{
			Assert.That(_lambda.Body, Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void MethodCallIsCheck()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;

			Assert.That(methodCall.Method, Is.EqualTo(_method));
		}

		[Then]
		public void MethodCallHasTwoArguments()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;

			Assert.That(methodCall.Arguments.Count, Is.EqualTo(2));
		}

		[Then]
		public void MethodCallFirstArgumentIsMethodCall()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;

			Assert.That(methodCall.Arguments[0], Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void MethodCallSecondArgumentIsConstant()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;

			Assert.That(methodCall.Arguments[1], Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void MethodCallSecondArgumentConstantIsValue()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;
			var secondArgument = (ConstantExpression) methodCall.Arguments[1];

			Assert.That(secondArgument.Value, Is.EqualTo(_value));
		}

		[Then]
		public void InnerMethodCallIsThatWithTargetType()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;
			var innerMethodCall = (MethodCallExpression) methodCall.Arguments[0];

			Assert.That(innerMethodCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void ThatCallHasOneArgument()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;
			var thatCall = (MethodCallExpression) methodCall.Arguments[0];

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void ThatCallArgumentIsTarget()
		{
			var methodCall = (MethodCallExpression) _lambda.Body;
			var thatCall = (MethodCallExpression) methodCall.Arguments[0];

			var target = _lambda.Parameters.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(target));
		}
	}
}