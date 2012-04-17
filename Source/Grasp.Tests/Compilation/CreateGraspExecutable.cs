using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Compilation
{
	public class CreateGraspExecutable : Behavior
	{
		GraspSchema _schema;
		ICalculator _calculator;
		GraspExecutable _executable;

		protected override void Given()
		{
			_schema = new GraspSchema(Enumerable.Empty<Variable>(), Enumerable.Empty<Calculation>());

			_calculator = A.Fake<ICalculator>();
		}

		protected override void When()
		{
			_executable = new GraspExecutable(_schema, _calculator);
		}

		[Then]
		public void HasOriginalSchema()
		{
			Assert.That(_executable.Schema, Is.EqualTo(_schema));
		}

		[Then]
		public void HasOriginalCalculator()
		{
			Assert.That(_executable.Calculator, Is.EqualTo(_calculator));
		}
	}
}