using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Grasp.Analysis.Compilation
{
	public class CompilationCycles
	{
		[Fact] public void Cycle()
		{
			var variableA = new Variable<int>("A");
			var variableB = new Variable<int>("B");
			var variableC = new Variable<int>("C");
			var calculationA = new Calculation(variableA, variableB.ToExpression());
			var calculationB = new Calculation(variableB, variableC.ToExpression());
			var calculationC = new Calculation(variableC, variableA.ToExpression());
			var schema = new GraspSchema(calculations: Params.Of(calculationA, calculationB, calculationC));

			var exception = Assert.Throws<CompilationException>(() => schema.Compile());

			exception.Schema.Should().Be(schema);
			exception.InnerException.Should().BeAssignableTo<CalculationCycleException>();

			var cycleException = (CalculationCycleException) exception.InnerException;

			cycleException.Context.SequenceEqual(Params.Of(calculationA, calculationB, calculationC)).Should().BeTrue();
			cycleException.RepeatedCalculation.Should().Be(calculationA);
		}

		[Fact] public void DeepCycle()
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

			var variableA = new Variable<int>("A");
			var variableB = new Variable<int>("B");
			var variableC = new Variable<int>("C");
			var variableD = new Variable<int>("D");
			var variableE = new Variable<int>("E");
			var variableF = new Variable<int>("F");
			var calculationA = new Calculation(variableA, Expression.Add(variableB.ToExpression(), variableC.ToExpression()));
			var calculationB = new Calculation(variableB, Expression.Add(variableD.ToExpression(), variableE.ToExpression()));
			var calculationC = new Calculation(variableC, Expression.Subtract(variableF.ToExpression(), Expression.Constant(1)));
			var calculationD = new Calculation(variableD, Expression.Constant(2));
			var calculationE = new Calculation(variableE, Expression.Add(variableC.ToExpression(), Expression.Constant(1)));
			var calculationF = new Calculation(variableF, Expression.Multiply(variableE.ToExpression(), Expression.Constant(2)));
			var schema = new GraspSchema(calculations: Params.Of(calculationA, calculationB, calculationC, calculationD, calculationE, calculationF));

			var exception = Assert.Throws<CompilationException>(() => schema.Compile());

			exception.Schema.Should().Be(schema);
			exception.InnerException.Should().BeAssignableTo<CalculationCycleException>();

			var cycleException = (CalculationCycleException) exception.InnerException;

			cycleException.Context.SequenceEqual(Params.Of(calculationA, calculationB, calculationE, calculationC, calculationF)).Should().BeTrue();
			cycleException.RepeatedCalculation.Should().Be(calculationE);
		}
	}
}