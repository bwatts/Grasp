using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Compilation
{
	public class CompileWithCycle : Behavior
	{
		Calculation _calculationA;
		Calculation _calculationB;
		Calculation _calculationC;
		GraspSchema _schema;
		CompilationException _exception;

		protected override void Given()
		{
			// A = B
			// B = C
			// C = A
			//
			// Cycle: A -> B -> C -> A

			var variableA = new Variable("Grasp", "A", typeof(int));
			var variableB = new Variable("Grasp", "B", typeof(int));
			var variableC = new Variable("Grasp", "C", typeof(int));

			_calculationA = new Calculation(variableA, Variable.Expression(variableB));
			_calculationB = new Calculation(variableB, Variable.Expression(variableC));
			_calculationC = new Calculation(variableC, Variable.Expression(variableA));

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { _calculationA, _calculationB, _calculationC });
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
		public void CalculationCycleInnerExceptionContextHasCalculationsInDependencyOrder()
		{
			var cycleException = (CalculationCycleException) _exception.InnerException;

			Assert.That(cycleException.Context.SequenceEqual(new[] { _calculationA, _calculationB, _calculationC }), Is.True);
		}

		[Then]
		public void CalculationCycleInnerExceptionHasCorrectRepeatedCalculation()
		{
			var cycleException = (CalculationCycleException) _exception.InnerException;

			Assert.That(cycleException.RepeatedCalculation, Is.EqualTo(_calculationA));
		}
	}
}