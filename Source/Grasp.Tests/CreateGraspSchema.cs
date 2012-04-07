using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp
{
	public class CreateGraspSchema : Behavior
	{
		IEnumerable<Variable> _variables;
		IEnumerable<Calculation> _calculations;
		GraspSchema _schema;

		protected override void Given()
		{
			_variables = new[] { new Variable("Grasp", "Test", typeof(int)), new Variable("Grasp", "Test2", typeof(int)) };

			_calculations = new[]
			{
				new Calculation(new Variable("Grasp", "TestOutput", typeof(int)), Expression.Constant(0)),
				new Calculation(new Variable("Grasp", "Test2Output", typeof(int)), Expression.Constant(1))
			};
		}

		protected override void When()
		{
			_schema = new GraspSchema(_variables, _calculations);
		}

		[Then]
		public void HasVariablesInOrder()
		{
			Assert.That(_schema.Variables.SequenceEqual(_variables), Is.True);
		}

		[Then]
		public void HasCalculationsInOrder()
		{
			Assert.That(_schema.Calculations.SequenceEqual(_calculations), Is.True);
		}
	}
}