using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Compilation
{
	public class CompileWithUnknownVariable : Behavior
	{
		Calculation _calculation;
		GraspSchema _schema;
		InvalidCalculationVariableException _exception;

		protected override void Given()
		{
			_calculation = new Calculation(new Variable("Grasp", "Output", typeof(int)), Variable.Expression(new Variable("Grasp", "Unknown", typeof(int))));

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { _calculation });
		}

		protected override void When()
		{
			try
			{
				_schema.Compile();
			}
			catch(InvalidCalculationVariableException ex)
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
		public void HasOriginalCalculation()
		{
			Assert.That(_exception.Calculation, Is.EqualTo(_calculation));
		}
	}
}