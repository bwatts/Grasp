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
	public class FieldRuleToLambda : Behavior
	{
		FieldInfo _field;
		MethodInfo _checkMethod;
		MemberRule _fieldRule;
		Type _targetType;
		MethodInfo _thatMethod;
		LambdaExpression _lambda;

		protected override void Given()
		{
			_field = Reflect.Field<TestTarget>(x => x.TargetField);

			_checkMethod = Reflect.Func<ICheckable<int>, Check<int>>(Checkable.IsEven);

			_fieldRule = Rule.Field(_field, Rule.Check(_checkMethod));

			_targetType = typeof(TestTarget);

			_thatMethod = typeof(Check)
				.GetMethods()
				.Where(method => method.Name == "That" && method.GetParameters().Count() == 1)
				.Single()
				.MakeGenericMethod(_field.FieldType);
		}

		protected override void When()
		{
			_lambda = _fieldRule.ToLambdaExpression(_targetType);
		}

		// Expecting: target => (bool) Check.That(target.TargetField).IsEven()

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

			Assert.That(checkCall.Method, Is.EqualTo(_checkMethod));
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
		public void ThatCallArgumentIsMemberAccess()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();

			Assert.That(thatCall.Arguments.Single(), Is.InstanceOf<MemberExpression>());
		}

		[Then]
		public void MemberAccessExpressionIsTargetParameter()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();
			var memberAccess = (MemberExpression) thatCall.Arguments.Single();

			Assert.That(memberAccess.Expression, Is.EqualTo(_lambda.Parameters.Single()));
		}

		[Then]
		public void MemberAccessMemberIsField()
		{
			var convert = (UnaryExpression) _lambda.Body;
			var checkCall = (MethodCallExpression) convert.Operand;
			var thatCall = (MethodCallExpression) checkCall.Arguments.Single();
			var memberAccess = (MemberExpression) thatCall.Arguments.Single();

			Assert.That(memberAccess.Member, Is.EqualTo(_field));
		}

		private class TestTarget
		{
			public int TargetField = 0;
		}
	}
}