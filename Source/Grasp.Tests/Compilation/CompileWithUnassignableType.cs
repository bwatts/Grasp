using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Compilation
{
	public class CompileWithUnassignableType : Behavior
	{
		Calculation _calculation;
		GraspSchema _schema;
		CompilationException _exception;

		protected override void Given()
		{
			_calculation = new Calculation(new Variable("Grasp", "Output", typeof(int)), Expression.Constant(""));

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { _calculation });
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
		public void HasInvalidCalculationResultTypeInnerException()
		{
			Assert.That(_exception.InnerException, Is.InstanceOf<InvalidCalculationResultTypeException>());
		}

		[Then]
		public void InnerInvalidCalculationResultTypeExceptionHasOriginalCalculation()
		{
			Assert.That(((InvalidCalculationResultTypeException) _exception.InnerException).Calculation, Is.EqualTo(_calculation));
		}
	}
}