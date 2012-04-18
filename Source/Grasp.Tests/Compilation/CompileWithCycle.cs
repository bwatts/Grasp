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
		Calculation _calculation1;
		Calculation _calculation2;
		Calculation _calculation3;
		GraspSchema _schema;
		CompilationException _exception;

		protected override void Given()
		{
			var variable1 = new Variable("Grasp", "Variable1", typeof(int));
			var variable2 = new Variable("Grasp", "Variable2", typeof(int));
			var variable3 = new Variable("Grasp", "Variable3", typeof(int));

			_calculation1 = new Calculation(variable1, Variable.Expression(variable2));
			_calculation2 = new Calculation(variable2, Variable.Expression(variable3));
			_calculation3 = new Calculation(variable3, Variable.Expression(variable1));

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { _calculation1, _calculation2, _calculation3 });
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
		public void CalculationCycleInnerExceptionHasCalculationsInDependencyOrder()
		{
			Assert.That(((CalculationCycleException) _exception.InnerException).Calculations.SequenceEqual(new[] { _calculation1, _calculation2, _calculation3 }), Is.True);
		}

		[Then]
		public void CalculationCycleInnerExceptionHasCorrectRepeatedCalculation()
		{
			Assert.That(((CalculationCycleException) _exception.InnerException).RepeatedCalculation, Is.EqualTo(_calculation1));
		}
	}
}