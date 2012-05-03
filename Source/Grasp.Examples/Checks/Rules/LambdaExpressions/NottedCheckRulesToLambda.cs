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
	public class NottedCheckRulesToLambda : Behavior
	{
		MethodInfo _method;
		NotRule _notRule;
		Type _targetType;
		MethodInfo _thatMethod;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_method = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsPositive);

			_notRule = Rule.Not(Rule.Check(_method));

			_targetType = typeof(int);

			_thatMethod = typeof(Check)
				.GetMethods()
				.Where(method => method.Name == "That" && method.GetParameters().Count() == 1)
				.Single()
				.MakeGenericMethod(_targetType);
		}

		protected override void When()
		{
			_lambda = _notRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => !((bool) Check.That(target).IsPositive())

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
		public void BodyNodeTypeIsNot()
		{
			Assert.That(_lambda.Body.NodeType, Is.EqualTo(ExpressionType.Not));
		}

		[Then]
		public void NotOperandIsConvert()
		{
			var not = (UnaryExpression) _lambda.Body;

			Assert.That(not.Operand.NodeType, Is.EqualTo(ExpressionType.Convert));
		}

		[Then]
		public void NotOperandConvertsToBoolean()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;

			Assert.That(convert.Type, Is.EqualTo(typeof(bool)));
		}

		[Then]
		public void ConvertOperandIsMethodCall()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;

			Assert.That(convert.Operand, Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void ConvertOperandMethodCallIsCheckCall()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Method, Is.EqualTo(_method));
		}

		[Then]
		public void CheckCallHasOneArgument()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void CheckCallArgumentIsMethodCall()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;
			var checkCall = (MethodCallExpression) convert.Operand;

			Assert.That(checkCall.Arguments.Single(), Is.InstanceOf<MethodCallExpression>());
		}

		[Then]
		public void CheckCallArgumentIsThatCall()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();

			Assert.That(thatCall.Method, Is.EqualTo(_thatMethod));
		}

		[Then]
		public void ThatCallHasOneArgument()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Count, Is.EqualTo(1));
		}

		[Then]
		public void ThatCallArgumentIsTargetParameter()
		{
			var not = (UnaryExpression) _lambda.Body;
			var convert = (UnaryExpression) not.Operand;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Single(), Is.EqualTo(_lambda.Parameters.Single()));
		}
	}
}