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
	public class AndedCheckRulesToLambda : Behavior
	{
		MethodInfo _method1;
		MethodInfo _method2;
		CheckRule _checkRule1;
		CheckRule _checkRule2;
		BinaryRule _andRule;
		Type _targetType;
		MethodInfo _thatMethod;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_method1 = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);
			_method2 = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsEven);

			_checkRule1 = Rule.Check(_method1);
			_checkRule2 = Rule.Check(_method2);

			_andRule = Rule.And(_checkRule1, _checkRule2);

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
		public void BodyMethodCallIsSecondCheck()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;

			Assert.That(secondCheckCall.Method, Is.EqualTo(_method2));
		}

		[Then]
		public void SecondCheckHasOneArgument()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;

			Assert.That(secondCheckCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void SecondCheckArgumentIsMethodCall()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;

			Assert.That(secondCheckCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void SecondCheckArgumentIsFirstCheck()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(firstCheckCall.Method, Is.EqualTo(_method1));
		}

		[Then]
		public void FirstCheckHasOneArgument()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(firstCheckCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void FirstCheckArgumentIsMethodCall()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(firstCheckCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void FirstCheckArgumentIsThatCall()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void ThatCallHasOneArgument()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void ThatCallArgumentIsTargetParameter()
		{
			var secondCheckCall = (MethodCallExpression) _lambda.Body;
			var firstCheckCall = (MethodCallExpression) secondCheckCall.Arguments.Single();
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(_lambda.Parameters.Single()));
		}
	}
}