using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis.Compilation
{
	public class CompileWithDeepCycle : Behavior
	{
		Calculation _calculationA;
		Calculation _calculationB;
		Calculation _calculationC;
		Calculation _calculationD;
		Calculation _calculationE;
		Calculation _calculationF;
		GraspSchema _schema;
		CompilationException _exception;

		protected override void Given()
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

			var variableA = new Variable("Grasp", "A", typeof(int));
			var variableB = new Variable("Grasp", "B", typeof(int));
			var variableC = new Variable("Grasp", "C", typeof(int));
			var variableD = new Variable("Grasp", "D", typeof(int));
			var variableE = new Variable("Grasp", "E", typeof(int));
			var variableF = new Variable("Grasp", "F", typeof(int));

			_calculationA = new Calculation(variableA, Expression.Add(Variable.Expression(variableB), Variable.Expression(variableC)));
			_calculationB = new Calculation(variableB, Expression.Add(Variable.Expression(variableD), Variable.Expression(variableE)));
			_calculationC = new Calculation(variableC, Expression.Subtract(Variable.Expression(variableF), Expression.Constant(1)));
			_calculationD = new Calculation(variableD, Expression.Constant(2));
			_calculationE = new Calculation(variableE, Expression.Add(Variable.Expression(variableC), Expression.Constant(1)));
			_calculationF = new Calculation(variableF, Expression.Multiply(Variable.Expression(variableE), Expression.Constant(2)));

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { _calculationA, _calculationB, _calculationC, _calculationD, _calculationE, _calculationF });
		}

		protected override void When()
		{
			try
			{
				_schema.Compile();
			}
			catch(CompilationException ex)
			{
				_exception = ex;
			}
		}

		[Then]
		public void Throws()
		{
			Assert.That(_exception, Is.Not.Null);
		}

		[Then]
		public void HasOriginatingSchema()
		{
			Assert.That(_exception.Schema, Is.EqualTo(_schema));
		}

		[Then]
		public void HasCalculationCycleInnerException()
		{
			Assert.That(_exception.InnerException, Is.InstanceOf<CalculationCycleException>());
		}

		[Then]
		public void CalculationCycleInnerExceptionHasContextCalculationsInDependencyOrder()
		{
			var cycleException = (CalculationCycleException) _exception.InnerException;

			Assert.That(cycleException.Context.SequenceEqual(new[] { _calculationA, _calculationB, _calculationE, _calculationC, _calculationF }), Is.True);
		}

		[Then]
		public void CalculationCycleInnerExceptionHasCorrectRepeatedCalculation()
		{
			var cycleException = (CalculationCycleException) _exception.InnerException;

			Assert.That(cycleException.RepeatedCalculation, Is.EqualTo(_calculationE));
		}
	}
}