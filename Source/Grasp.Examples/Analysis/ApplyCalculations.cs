using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Grasp.Analysis.Compilation;
using Xbehave;

namespace Grasp.Analysis
{
	public class ApplyCalculations
	{
		[Scenario]
		public void Apply(GraspSchema schema, ICalculator calculator, GraspRuntime runtime)
		{
			"Given an empty schema".Given(() => schema = new GraspSchema());
			"And a calculator".And(() => calculator = A.Fake<ICalculator>());
			"And a runtime with the schema and calculator".And(() => runtime = new GraspRuntime(schema, calculator));

			"When applying calculations to the runtime".When(() => runtime.ApplyCalculations());

			"It applies itself to the calculator".Then(() => A.CallTo(() => calculator.ApplyCalculation(runtime)).MustHaveHappened(Repeated.Exactly.Once));
		}

		[Scenario]
		public void ApplySingle(int left, int right, Variable outputVariable, GraspSchema schema, GraspRuntime runtime)
		{
			"Given a left value".Given(() => left = 1);
			"And a right value".And(() => right = 2);
			"And an output variable".And(() => outputVariable = new Variable<int>("X"));
			"And a schema with a calculation".And(() => schema = new GraspSchema(
				calculations: Params.Of(new Calculation(outputVariable, Expression.Add(Expression.Constant(left), Expression.Constant(right))))));
			"And a runtime from the compiled executable".And(() => runtime = schema.Compile().GetRuntime(A.Fake<IRuntimeSnapshot>()));

			"When applying calculations to the runtime".When(() => runtime.ApplyCalculations());

			"It calculates and sets the output variable".Then(() => runtime.GetVariableValue(outputVariable).Should().Be(left + right));
		}

		[Scenario]
		public void ApplySingleWithVariable(int left, int right, Variable leftVariable, Variable outputVariable, GraspSchema schema, IRuntimeSnapshot initialState, GraspRuntime runtime)
		{
			"Given a left value".Given(() => left = 1);
			"And a right value".And(() => right = 2);
			"And a left variable".And(() => leftVariable = new Variable<int>("X"));
			"And an output variable".And(() => outputVariable = new Variable<int>("Y"));
			"And a schema with the left variable and a calculation".And(() => schema = new GraspSchema(
				Params.Of<Variable>(leftVariable),
				Params.Of(new Calculation(outputVariable, Expression.Add(Variable.Expression(leftVariable), Expression.Constant(right))))));
			"And an initial state with a value for the left variable".And(() =>
			{
				initialState = A.Fake<IRuntimeSnapshot>();

				A.CallTo(() => initialState.GetValue(leftVariable)).Returns(left);
			});
			"And a runtime from the compiled executable".And(() => runtime = schema.Compile().GetRuntime(initialState));

			"When applying calculations to the runtime".When(() => runtime.ApplyCalculations());

			"It calculates and sets the output variable".Then(() => runtime.GetVariableValue(outputVariable).Should().Be(left + right));
		}

		[Scenario]
		public void ApplySingleWithTwoVariables(int left, int right, Variable leftVariable, Variable rightVariable, Variable outputVariable, GraspSchema schema, IRuntimeSnapshot initialState, GraspRuntime runtime)
		{
			"Given a left value".Given(() => left = 1);
			"And a right value".And(() => right = 2);
			"And a left variable".And(() => leftVariable = new Variable<int>("X"));
			"And a right variable".And(() => rightVariable = new Variable<int>("Y"));
			"And an output variable".And(() => outputVariable = new Variable<int>("Z"));
			"And a schema with the left and right variables and a calculation".And(() => schema = new GraspSchema(
				Params.Of<Variable>(leftVariable, rightVariable),
				Params.Of(new Calculation(outputVariable, Expression.Add(Variable.Expression(leftVariable), Variable.Expression(rightVariable))))));
			"And an initial state with values for the left and right variables".And(() =>
			{
				initialState = A.Fake<IRuntimeSnapshot>();

				A.CallTo(() => initialState.GetValue(leftVariable)).Returns(left);
				A.CallTo(() => initialState.GetValue(rightVariable)).Returns(right);
			});
			"And a runtime from the compiled executable".And(() => runtime = schema.Compile().GetRuntime(initialState));

			"When applying calculations to the runtime".When(() => runtime.ApplyCalculations());

			"It calculates and sets the output variable".Then(() => runtime.GetVariableValue(outputVariable).Should().Be(left + right));
		}

		[Scenario]
		public void ApplyMultiple(int left1, int right1, int left2, int right2, Variable outputVariable1, Variable outputVariable2, GraspSchema schema, GraspRuntime runtime)
		{
			"Given a left value".Given(() => left1 = 1);
			"And a right value".And(() => right1 = 2);
			"And a second left value".And(() => left2 = 11);
			"And a second right value".And(() => right2 = 12);
			"And an output variable".And(() => outputVariable1 = new Variable<int>("X"));
			"And a second output variable".And(() => outputVariable2 = new Variable<int>("Y"));
			"And a schema with a pair of calculations".And(() => schema = new GraspSchema(
				calculations: Params.Of(
					new Calculation(outputVariable1, Expression.Add(Expression.Constant(left1), Expression.Constant(right1))),
					new Calculation(outputVariable2, Expression.Add(Expression.Constant(left2), Expression.Constant(right2))))));
			"And a runtime from the compiled executable".And(() => runtime = schema.Compile().GetRuntime(A.Fake<IRuntimeSnapshot>()));

			"When applying calculations to the runtime".When(() => runtime.ApplyCalculations());

			"It calculates and sets the first output variable".Then(() => runtime.GetVariableValue(outputVariable1).Should().Be(left1 + right1));
			"It calculates and sets the second output variable".Then(() => runtime.GetVariableValue(outputVariable2).Should().Be(left2 + right2));
		}

		[Scenario]
		public void ApplyDependent(int left1, int right1, int left2, Variable outputVariable1, Variable outputVariable2, GraspSchema schema, GraspRuntime runtime)
		{
			"Given a left value".Given(() => left1 = 1);
			"And a right value".And(() => right1 = 2);
			"And a second left value".And(() => left2 = 10);
			"And an output variable".And(() => outputVariable1 = new Variable<int>("X"));
			"And a second output variable".And(() => outputVariable2 = new Variable<int>("Y"));
			"And a schema with a pair of dependent calculations".And(() => schema = new GraspSchema(
				calculations: Params.Of(
					new Calculation(outputVariable1, Expression.Add(Expression.Constant(left1), Expression.Constant(right1))),
					new Calculation(outputVariable2, Expression.Add(Expression.Constant(left2), Variable.Expression(outputVariable1))))));
			"And a runtime from the compiled executable".And(() => runtime = schema.Compile().GetRuntime(A.Fake<IRuntimeSnapshot>()));

			"When applying calculations to the runtime".When(() => runtime.ApplyCalculations());

			"It calculates and sets the first output variable".Then(() => runtime.GetVariableValue(outputVariable1).Should().Be(left1 + right1));
			"It calculates and sets the second output variable".Then(() =>
			{
				var outputVariable1Value = (int) runtime.GetVariableValue(outputVariable1);

				runtime.GetVariableValue(outputVariable2).Should().Be(left2 + outputVariable1Value);
			});
		}
	}
}