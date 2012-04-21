using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using Grasp.Analysis.Compilation;
using NUnit.Framework;

namespace Grasp.Analysis
{
	public class CompileGraspSchema : Behavior
	{
		IEnumerable<Variable> _variables;
		IEnumerable<Calculation> _calculations;
		GraspSchema _schema;
		GraspExecutable _executable;

		protected override void Given()
		{
			_variables = new[] { new Variable("Grasp", "Test", typeof(int)), new Variable("Grasp", "Test2", typeof(int)) };

			_calculations = new[]
			{
				new Calculation(new Variable("Grasp", "TestOutput", typeof(int)), Expression.Constant(0)),
				new Calculation(new Variable("Grasp", "Test2Output", typeof(int)), Expression.Constant(1))
			};

			_schema = new GraspSchema(_variables, _calculations);
		}

		protected override void When()
		{
			_executable = _schema.Compile();
		}

		[Then]
		public void ExecutableIsNotNull()
		{
			Assert.That(_executable, Is.Not.Null);
		}

		[Then]
		public void HasOriginalSchema()
		{
			Assert.That(_executable.Schema, Is.EqualTo(_schema));
		}
	}
}