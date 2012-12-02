using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using Cloak.Reflection;
using FluentAssertions;
using Grasp.Checks;
using Xunit;

namespace Grasp.Knowledge.Definition
{
	public class RankQuestions
	{
		[Fact] public void Create()
		{
			var item1 = new Identifier("SomeItem1");
			var item2 = new Identifier("SomeItem2");
			var validVariableName = new Identifier("Valid");

			var question = new RankQuestion(Params.Of(item1, item2), validVariableName);

			question.ItemVariableNames.Should().ContainInOrder(item1, item2);
			question.ValidVariableName.Should().Be(validVariableName);
		}

		[Fact] public void GetSchema()
		{
			var question = new RankQuestion(Params.Of(new Identifier("SomeItem1"), new Identifier("SomeItem2")), new Identifier("Valid"));

			var schema = question.GetSchema("SomeRank");

			schema.ShouldHaveVariables("SomeRank.SomeItem1", "SomeRank.SomeItem2");

			schema.Calculations.Should().HaveCount(1);

			var calculation = schema.Calculations.Single();

			calculation.ShouldCalculate("SomeRank.__validation.Valid", typeof(bool));

			calculation.Expression.Should().BeAssignableTo<InvocationExpression>();

			// AreDistinct check

			var invocation = (InvocationExpression) calculation.Expression;

			invocation.Expression.Should().BeAssignableTo<LambdaExpression>();

			var areDistinctLambda = (LambdaExpression) invocation.Expression;

			areDistinctLambda.Parameters.Should().HaveCount(1);
			areDistinctLambda.Parameters.Single().Type.Should().Be(typeof(int[]));

			areDistinctLambda.Body.Should().BeAssignableTo<UnaryExpression>();

			var convert = (UnaryExpression) areDistinctLambda.Body;

			convert.NodeType.Should().Be(ExpressionType.Convert);
			convert.Type.Should().Be(typeof(bool));

			convert.Operand.Should().BeAssignableTo<MethodCallExpression>();

			var areDistinctCall = (MethodCallExpression) convert.Operand;

			areDistinctCall.Method.Should().Be(Reflect.Func<ICheckable<IEnumerable<int>>, Check<IEnumerable<int>>>(Checkable.AreDistinct));

			areDistinctCall.Arguments.Should().HaveCount(1);
			areDistinctCall.Arguments.Single().Should().BeAssignableTo<MethodCallExpression>();

			var thatCall = (MethodCallExpression) areDistinctCall.Arguments.Single();

			thatCall.Method.Should().Be(Reflect.Func<int[], bool, ICheckable<int[]>>(Check.That));
			
			// Values

			invocation.Arguments.Should().HaveCount(1);
			invocation.Arguments.Single().Should().BeAssignableTo<NewArrayExpression>();

			var newArray = (NewArrayExpression) invocation.Arguments.Single();

			newArray.Type.Should().Be(typeof(int[]));
			newArray.Expressions.Should().HaveCount(2);
			newArray.Expressions.ElementAt(0).Should().BeAssignableTo<VariableExpression>();
			newArray.Expressions.ElementAt(1).Should().BeAssignableTo<VariableExpression>();

			var item1 = (VariableExpression) newArray.Expressions.ElementAt(0);

			item1.Variable.Type.Should().Be(typeof(int));
			item1.Variable.Name.Value.Should().Be("SomeRank.SomeItem1");

			var item2 = (VariableExpression) newArray.Expressions.ElementAt(1);

			item2.Variable.Type.Should().Be(typeof(int));
			item2.Variable.Name.Value.Should().Be("SomeRank.SomeItem2");
		}
	}
}