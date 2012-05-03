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
	[Ignore]
	public class AndedCheckRulesToLambda : Behavior
	{
		MethodInfo _method1;
		MethodInfo _method2;
		BinaryRule _andRule;
		Type _targetType;
		MethodInfo _thatMethod;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_method1 = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);
			_method2 = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsEven);

			_andRule = Rule.And(Rule.Check(_method1), Rule.Check(_method2));

			_targetType = typeof(int);

			_thatMethod = typeof(Check)
				.GetMethods()
				.Where(method => method.Name == "That" && method.GetParameters().Count() == 1)
				.Single()
				.MakeGenericMethod(_targetType);
		}

		protected override void When()
		{
			_lambda = _andRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => (bool) Check.That(target).IsPositive().IsEven()

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
		public void ConvertOperandMethodCallIsSecondCheckCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(secondCheckCall.Method, Is.EqualTo(_method2));
		}

		[Then]
		public void SecondCheckCallHasOneArgument()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(secondCheckCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void SecondCheckCallArgumentIsMethodCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(secondCheckCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void SecondCheckCallArgumentIsFirstCheck()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(firstCheckCall.Method, Is.EqualTo(_method1));
		}

		[Then]
		public void FirstCheckCallHasOneArgument()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(firstCheckCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void FirstCheckCallArgumentIsMethodCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(firstCheckCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void FirstCheckCallArgumentIsThatCall()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void ThatCallHasOneArgument()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void ThatCallArgumentIsTargetParameter()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(_lambda.Parameters.Single()));
		}
	}
}