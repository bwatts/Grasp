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
	public class OredCheckRulesToLambda : Behavior
	{
		MethodInfo _method1;
		MethodInfo _method2;
		BinaryRule _orRule;
		Type _targetType;
		MethodInfo _thatMethod;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_method1 = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);
			_method2 = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsEven);

			_orRule = Rule.Or(Rule.Check(_method1), Rule.Check(_method2));

			_targetType = typeof(int);

			_thatMethod = typeof(Check)
				.GetMethods()
				.Where(method => method.Name == "That" && method.GetParameters().Count() == 1)
				.Single()
				.MakeGenericMethod(_targetType);
		}

		protected override void When()
		{
			_lambda = _orRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => (bool) Check.That(target).IsPositive() || (bool) Check.That(target).IsEven()

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
		public void OrElseLeftIsConvert()
		{
			var orElse = (BinaryExpression) _lambda.Body;

			Assert.That(orElse.Left.NodeType, Is.EqualTo(ExpressionType.Convert));
		}

		[Then]
		public void OrElseLeftConvertsToBoolean()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;

			Assert.That(convert.Type, Is.EqualTo(typeof(bool)));
		}

		[Then]
		public void LeftConvertOperandIsMethodCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;

			Assert.That(convert.Operand, Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void LeftConvertOperandIsFirstCheckCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;
			var firstCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(firstCheckCall.Method, Is.EqualTo(_method1));
		}

		[Then]
		public void FirstCheckCallHasOneArgument()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;
			var firstCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(firstCheckCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void FirstCheckCallArgumentIsMethodCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;
			var firstCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(firstCheckCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void FirstCheckCallArgumentIsThatCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;
			var firstCheckCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void FirstThatCallHasOneArgument()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;
			var firstCheckCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void FirstThatCallArgumentIsTargetParameter()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Left;
			var firstCheckCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) firstCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(_lambda.Parameters.Single()));
		}

		[Then]
		public void OrElseRightIsConvert()
		{
			var orElse = (BinaryExpression) _lambda.Body;

			Assert.That(orElse.Right.NodeType, Is.EqualTo(ExpressionType.Convert));
		}

		[Then]
		public void OrElseRightConvertsToBoolean()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;

			Assert.That(convert.Type, Is.EqualTo(typeof(bool)));
		}

		[Then]
		public void RightConvertOperandIsMethodCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;

			Assert.That(convert.Operand, Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void RightConvertOperandIsSecondCheckCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;
			var secondCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(secondCheckCall.Method, Is.EqualTo(_method2));
		}

		[Then]
		public void OrElseRightIsSecondCheckCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;
			var secondCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(secondCheckCall.Method, Is.EqualTo(_method2));
		}

		[Then]
		public void SecondCheckCallHasOneArgument()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;
			var secondCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(secondCheckCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void SecondCheckCallArgumentIsMethodCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;
			var secondCheckCall = (MethodCallExpression) convert.Operand;

			Assert.That(secondCheckCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void SecondCheckCallArgumentIsThatCall()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(thatCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void SecondThatCallHasOneArgument()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void SecondThatCallArgumentIsTargetParameter()
		{
			var orElse = (BinaryExpression) _lambda.Body;
			var convert = (UnaryExpression) orElse.Right;
			var secondCheckCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) secondCheckCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(_lambda.Parameters.Single()));
		}
	}
}