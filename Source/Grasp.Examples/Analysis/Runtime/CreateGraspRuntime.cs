using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Analysis.Runtime
{
	public class CreateGraspRuntime : Behavior
	{
		IEnumerable<Variable> _variables;
		IEnumerable<Calculation> _calculations;
		GraspSchema _schema;
		ICalculator _calculator;
		GraspRuntime _runtime;

		protected override void Given()
		{
			_variables = new[] { new Variable("Grasp", "Test", typeof(int)), new Variable("Grasp", "Test2", typeof(int)) };

			_calculations = new[]
			{
				new Calculation(new Variable("Grasp", "TestOutput", typeof(int)), Expression.Constant(0)),
				new Calculation(new Variable("Grasp", "Test2Output", typeof(int)), Expression.Constant(1))
			};

			_schema = new GraspSchema(_variables, _calculations);

			_calculator = A.Fake<ICalculator>();
		}

		protected override void When()
		{
			_runtime = new GraspRuntime(_schema, _calculator, Enumerable.Empty<VariableBinding>());
		}

		[Then]
		public void HasOriginalSchema()
		{
			Assert.That(_runtime.Schema, Is.EqualTo(_schema));
		}

		[Then]
		public void HasOriginalCalculator()
		{
			Assert.That(_runtime.Calculator, Is.EqualTo(_calculator));
		}
	}
}