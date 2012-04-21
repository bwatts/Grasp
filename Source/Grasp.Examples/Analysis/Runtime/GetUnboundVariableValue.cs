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
	public class GetUnboundVariableValue : Behavior
	{
		Variable _variable;
		GraspSchema _schema;
		ICalculator _calculator;
		GraspRuntime _runtime;
		UnboundVariableException _exception;

		protected override void Given()
		{
			_variable = new Variable("Grasp", "Test", typeof(int));

			_schema = new GraspSchema(new[] { _variable }, Enumerable.Empty<Calculation>());

			_calculator = A.Fake<ICalculator>();

			_runtime = new GraspRuntime(_schema, _calculator, Enumerable.Empty<VariableBinding>());
		}

		protected override void When()
		{
			try
			{
				_runtime.GetVariableValue(_variable);
			}
			catch(UnboundVariableException ex)
			{
				_exception = ex;
			}
		}

		[Then]
		public void Throws()
		{
			Assert.That(_exception, Is.Not.Null);
		}
	}
}