using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Grasp.Knowledge.Runtime.Compilation;
using Xunit;

namespace Grasp.Knowledge.Runtime
{
	public class ApplyCalculations
	{
		[Fact] public void Apply()
		{
			var schema = new Schema();
			var calculator = A.Fake<ICalculator>();
			var binding = new SchemaBinding(schema, calculator);

			binding.ApplyCalculations();

			A.CallTo(() => calculator.ApplyCalculation(binding)).MustHaveHappened(Repeated.Exactly.Once);
		}

		[Fact] public void ApplySingle()
		{
			var left = 1;
			var right = 2;
			var outputVariable = new Variable<int>("A");
			var schema = new Schema(calculations: Params.Of(new Calculation(outputVariable, Expression.Add(Expression.Constant(left), Expression.Constant(right)))));
			var binding = schema.Compile().Bind(new TestSnapshot());

			binding.ApplyCalculations();

			binding.GetVariableValue(outputVariable.Name).Should().Be(left + right);
		}

		[Fact] public void ApplySingleWithVariable()
		{
			var left = 1;
			var right = 2;
			var leftVariable = new Variable<int>("A");
			var outputVariable = new Variable<int>("B");
			var schema = new Schema(
				Params.Of<Variable>(leftVariable),
				Params.Of(new Calculation(outputVariable, Expression.Add(leftVariable.ToExpression(), Expression.Constant(right)))));
			var initialState = new TestSnapshot { AValue = left };
			var binding = schema.Compile().Bind(initialState);

			binding.ApplyCalculations();

			binding.GetVariableValue(outputVariable.Name).Should().Be(left + right);
		}

		[Fact] public void ApplySingleWithTwoVariables()
		{
			var left = 1;
			var right = 2;
			var leftVariable = new Variable<int>("A");
			var rightVariable = new Variable<int>("B");
			var outputVariable = new Variable<int>("C");
			var schema = new Schema(
				Params.Of<Variable>(leftVariable, rightVariable),
				Params.Of(new Calculation(outputVariable, Expression.Add(leftVariable.ToExpression(), rightVariable.ToExpression()))));
			var initialState = new TestSnapshot { AValue = left, BValue = right };
			var binding = schema.Compile().Bind(initialState);

			binding.ApplyCalculations();

			binding.GetVariableValue(outputVariable.Name).Should().Be(left + right);
		}

		[Fact] public void ApplyMultiple()
		{
			var left1 = 1;
			var right1 = 2;
			var left2 = 11;
			var right2 = 12;
			var outputVariable1 = new Variable<int>("A");
			var outputVariable2 = new Variable<int>("B");
			var schema = new Schema(calculations: Params.Of(
				new Calculation(outputVariable1, Expression.Add(Expression.Constant(left1), Expression.Constant(right1))),
				new Calculation(outputVariable2, Expression.Add(Expression.Constant(left2), Expression.Constant(right2)))));
			var binding = schema.Compile().Bind(new TestSnapshot());

			binding.ApplyCalculations();

			binding.GetVariableValue(outputVariable1.Name).Should().Be(left1 + right1);
			binding.GetVariableValue(outputVariable2.Name).Should().Be(left2 + right2);
		}

		[Fact] public void ApplyDependent()
		{
			var left1 = 1;
			var right1 = 2;
			var left2 = 10;
			var outputVariable1 = new Variable<int>("A");
			var outputVariable2 = new Variable<int>("B");
			var schema = new Schema(calculations: Params.Of(
				new Calculation(outputVariable1, Expression.Add(Expression.Constant(left1), Expression.Constant(right1))),
				new Calculation(outputVariable2, Expression.Add(Expression.Constant(left2), outputVariable1.ToExpression()))));
			var binding = schema.Compile().Bind(new TestSnapshot());

			binding.ApplyCalculations();

			binding.GetVariableValue(outputVariable1.Name).Should().Be(left1 + right1);

			var outputVariable1Value = (int) binding.GetVariableValue(outputVariable1.Name);

			binding.GetVariableValue(outputVariable2.Name).Should().Be(left2 + outputVariable1Value);
		}

		private sealed class TestSnapshot : ISnapshot
		{
			internal int AValue { get; set; }
			internal int BValue { get; set; }

			public object GetValue(FullName name)
			{
				return name.Value == "A" ? AValue : BValue;
			}
		}
	}
}