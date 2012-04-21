using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis.Compilation
{
	public class CompileWithInvalidExpression : Behavior
	{
		GraspSchema _schema;
		CompilationException _exception;

		protected override void Given()
		{
			var calculation = new Calculation(new Variable("Grasp", "Output", typeof(int)), new InvalidExpression());

			_schema = new GraspSchema(Enumerable.Empty<Variable>(), new[] { calculation });
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

		private sealed class InvalidExpression : Expression
		{
			public override ExpressionType NodeType
			{
				get { return (ExpressionType) (-1); }
			}
		}
	}
}