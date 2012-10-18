using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis.Compilation
{
	public class CompilationCycles
	{
		[Scenario]
		public void Cycle(Variable variableA, Variable variableB, Variable variableC, Calculation calculationA, Calculation calculationB, Calculation calculationC, GraspSchema schema, CompilationException exception)
		{
			"Given a variable A".Given(() => variableA = new Variable<int>("A"));
			"And a variable B".And(() => variableB = new Variable<int>("B"));
			"And a variable C".And(() => variableC = new Variable<int>("C"));
			"And the calculation 'A = B'".And(() => calculationA = new Calculation(variableA, Variable.Expression(variableB)));
			"And the calculation 'B = C'".And(() => calculationB = new Calculation(variableB, Variable.Expression(variableC)));
			"And the calculation 'C = A'".And(() => calculationC = new Calculation(variableC, Variable.Expression(variableA)));
			"And a schema with the set of calculations forming a cycle (A -> B -> C -> A)".And(() => schema = new GraspSchema(calculations: Params.Of(calculationA, calculationB, calculationC)));

			"When compiling an executable from the schema".When(() =>
			{
				try
				{
					schema.Compile();
				}
				catch(CompilationException ex)
				{
					exception = ex;
				}
			});

			"It throws an exception".Then(() => exception.Should().NotBeNull());
			"It has the specified schema".Then(() => exception.Schema.Should().Be(schema));
			"It has a calculation cycle inner exception".Then(() => exception.InnerException.Should().BeAssignableTo<CalculationCycleException>());
			"The calculation cycle inner exception has the calculations in order of dependency".Then(() =>
			{
				var cycleException = (CalculationCycleException) exception.InnerException;

				cycleException.Context.SequenceEqual(Params.Of(calculationA, calculationB, calculationC)).Should().BeTrue();
			});
			"It has the correct repeated calculation".Then(() =>
			{
				var cycleException = (CalculationCycleException) exception.InnerException;

				cycleException.RepeatedCalculation.Should().Be(calculationA);
			});
		}

		[Scenario]
		public void DeepCycle(
			Variable variableA, Variable variableB, Variable variableC, Variable variableD, Variable variableE, Variable variableF,
			Calculation calculationA, Calculation calculationB, Calculation calculationC, Calculation calculationD, Calculation calculationE, Calculation calculationF,
			GraspSchema schema, CompilationException exception)
		{
			// A = B + C
			// B = D + E
			// C = F - 1
			// D = 2
			// E = C + 1
			// F = E * 2
			//
			// Cycle: E -> C -> F -> E
			//
			// Path from root: A -> B -> E -> C -> F -> E

			"Given a variable A".Given(() => variableA = new Variable<int>("A"));
			"And a variable B".And(() => variableB = new Variable<int>("B"));
			"And a variable C".And(() => variableC = new Variable<int>("C"));
			"And a variable D".And(() => variableD = new Variable<int>("D"));
			"And a variable E".And(() => variableE = new Variable<int>("E"));
			"And a variable F".And(() => variableF = new Variable<int>("F"));
			"And a calculation which outputs variable A".And(() => calculationA = new Calculation(variableA, Expression.Add(Variable.Expression(variableB), Variable.Expression(variableC))));
			"And a calculation which outputs variable B".And(() => calculationB = new Calculation(variableB, Expression.Add(Variable.Expression(variableD), Variable.Expression(variableE))));
			"And a calculation which outputs variable C".And(() => calculationC = new Calculation(variableC, Expression.Subtract(Variable.Expression(variableF), Expression.Constant(1))));
			"And a calculation which outputs variable D".And(() => calculationD = new Calculation(variableD, Expression.Constant(2)));
			"And a calculation which outputs variable E".And(() => calculationE = new Calculation(variableE, Expression.Add(Variable.Expression(variableC), Expression.Constant(1))));
			"And a calculation which outputs variable F".And(() => calculationF = new Calculation(variableF, Expression.Multiply(Variable.Expression(variableE), Expression.Constant(2))));
			"And a schema with the set of calculations forming a cycle (E -> C -> F -> E".And(() => schema = new GraspSchema(calculations: Params.Of(calculationA, calculationB, calculationC, calculationD, calculationE, calculationF)));

			"When compiling an executable from the schema".When(() =>
			{
				try
				{
					schema.Compile();
				}
				catch(CompilationException ex)
				{
					exception = ex;
				}
			});

			"It throws an exception".Then(() => exception.Should().NotBeNull());
			"It has the specified schema".Then(() => exception.Schema.Should().Be(schema));

			var cycleException = default(CalculationCycleException);
			
			"It has a calculation cycle inner exception".Then(() =>
			{
				exception.InnerException.Should().BeAssignableTo<CalculationCycleException>();

				cycleException = (CalculationCycleException) exception.InnerException;
			});

			"The cycle exception has the calculations in order of dependency".Then(
				() => cycleException.Context.SequenceEqual(Params.Of(calculationA, calculationB, calculationE, calculationC, calculationF)).Should().BeTrue());

			"The cycle exception has the repeated calculation".Then(() => cycleException.RepeatedCalculation.Should().Be(calculationE));
		}
	}
}