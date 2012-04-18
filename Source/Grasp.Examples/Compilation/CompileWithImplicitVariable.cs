using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Compilation
{
	public class CompileWithImplicitVariable : Behavior
	{
		GraspSchema _schema;
		GraspExecutable _executable;

		protected override void Given()
		{
			var implicitVariable = new Variable("Grasp", "Implicit", typeof(int));

			var calculation1 = new Calculation(implicitVariable, Expression.Constant(1));
			var calculation2 = new Calculation(new Variable("Grasp", "Output", typeof(int)), Variable.Expression(implicitVariable));

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { calculation1, calculation2 });
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
	}
}