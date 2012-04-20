using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Compilation
{
	public class CompileWithDerivedType : Behavior
	{
		Derived _result;
		Calculation _calculation;
		GraspSchema _schema;
		GraspExecutable _executable;

		protected override void Given()
		{
			_result = new Derived();

			_calculation = new Calculation(new Variable("Grasp", "Output", typeof(Base)), Expression.Constant(_result));

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { _calculation });
		}

		protected override void When()
		{
			_executable = _schema.Compile();
		}

		[Then]
		public void Succeeds()
		{
			Assert.That(_executable, Is.Not.Null);
		}

		private class Base
		{}

		private class Derived : Base
		{}
	}
}