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

		// Expecting: target => (bool) Check.That(target).StartsWith("value")

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
		public void CheckCallHasTwoArguments()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Arguments.Count, Is.EqualTo(2));
		}

		[Then]
		public void CheckCallFirstArgumentIsMethodCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Arguments[0], Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void CheckCallFirstArgumentIsThatCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments[0];

			Assert.That(thatCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void ThatCallHasOneArgument()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments[0];

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void ThatCallArgumentIsTargetParameter()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments[0];

			var target = _lambda.Parameters.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(target));
		}

		[Then]
		public void CheckCallSecondArgumentIsConstant()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Arguments[1], Is.InstanceOf<ConstantExpression>());
		}

		[Then]
		public void CheckCallSecondArgumentConstantIsValue()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var secondArgument = (ConstantExpression) checkCall.Arguments[1];

			Assert.That(secondArgument.Value, Is.EqualTo(_value));
		}
	}
}