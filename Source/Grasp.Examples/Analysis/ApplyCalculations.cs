using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Grasp.Analysis.Compilation;
using Xunit;

namespace Grasp.Analysis
{
	public class ApplyCalculations
	{
		[Fact] public void Apply()
		{
			var schema = new GraspSchema();
			var calculator = A.Fake<ICalculator>();
			var runtime = new GraspRuntime(schema, calculator);

			runtime.ApplyCalculations();

			A.CallTo(() => calculator.ApplyCalculation(runtime)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Fact] public void ApplySingle()
		{
			var left = 1;
			var right = 2;
			var outputVariable = new Variable<int>("X");
			var schema = new GraspSchema(calculations: Params.Of(new Calculation(outputVariable, Expression.Add(Expression.Constant(left), Expression.Constant(right)))));
			var runtime = schema.Compile().GetRuntime(new TestSnapshot());

			runtime.ApplyCalculations();

			runtime.GetVariableValue(outputVariable).Should().Be(left + right);
		}

		[Fact] public void ApplySingleWithVariable()
		{
			var left = 1;
			var right = 2;
			var leftVariable = new Variable<int>("X");
			var outputVariable = new Variable<int>("Y");
			var schema = new GraspSchema(
				Params.Of<Variable>(leftVariable),
				Params.Of(new Calculation(outputVariable, Expression.Add(leftVariable.ToExpression(), Expression.Constant(right)))));
			var initialState = new TestSnapshot { XValue = left };
			var runtime = schema.Compile().GetRuntime(initialState);

			runtime.ApplyCalculations();

			runtime.GetVariableValue(outputVariable).Should().Be(left + right);
		}

		[Fact] public void ApplySingleWithTwoVariables()
		{
			var left = 1;
			var right = 2;
			var leftVariable = new Variable<int>("X");
			var rightVariable = new Variable<int>("Y");
			var outputVariable = new Variable<int>("Z");
			var schema = new GraspSchema(
				Params.Of<Variable>(leftVariable, rightVariable),
				Params.Of(new Calculation(outputVariable, Expression.Add(leftVariable.ToExpression(), rightVariable.ToExpression()))));
			var initialState = new TestSnapshot { XValue = left, YValue = right };
			var runtime = schema.Compile().GetRuntime(initialState);

			runtime.ApplyCalculations();

			runtime.GetVariableValue(outputVariable).Should().Be(left + right);
		}

		[Fact] public void ApplyMultiple()
		{
			var left1 = 1;
			var right1 = 2;
			var left2 = 11;
			var right2 = 12;
			var outputVariable1 = new Variable<int>("X");
			var outputVariable2 = new Variable<int>("Y");
			var schema = new GraspSchema(calculations: Params.Of(
				new Calculation(outputVariable1, Expression.Add(Expression.Constant(left1), Expression.Constant(right1))),
				new Calculation(outputVariable2, Expression.Add(Expression.Constant(left2), Expression.Constant(right2)))));
			var runtime = schema.Compile().GetRuntime(new TestSnapshot());

			runtime.ApplyCalculations();

			runtime.GetVariableValue(outputVariable1).Should().Be(left1 + right1);
			runtime.GetVariableValue(outputVariable2).Should().Be(left2 + right2);
		}

		[Fact] public void ApplyDependent()
		{
			var left1 = 1;
			var right1 = 2;
			var left2 = 10;
			var outputVariable1 = new Variable<int>("X");
			var outputVariable2 = new Variable<int>("Y");
			var schema = new GraspSchema(calculations: Params.Of(
				new Calculation(outputVariable1, Expression.Add(Expression.Constant(left1), Expression.Constant(right1))),
				new Calculation(outputVariable2, Expression.Add(Expression.Constant(left2), outputVariable1.ToExpression()))));
			var runtime = schema.Compile().GetRuntime(new TestSnapshot());

			runtime.ApplyCalculations();

			runtime.GetVariableValue(outputVariable1).Should().Be(left1 + right1);
			
			var outputVariable1Value = (int) runtime.GetVariableValue(outputVariable1);

			runtime.GetVariableValue(outputVariable2).Should().Be(left2 + outputVariable1Value);
		}

		private sealed class TestSnapshot : IRuntimeSnapshot
		{
			internal int XValue { get; set; }
			internal int YValue { get; set; }

			public object GetValue(Variable variable)
			{
				return variable.Name == "X" ? XValue : YValue;
			}
		}
	}
}