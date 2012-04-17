using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Runtime
{
	public class ApplyCalculations : Behavior
	{
		GraspSchema _schema;
		ICalculator _calculator;
		GraspRuntime _runtime;

		protected override void Given()
		{
			_schema = new GraspSchema(Enumerable.Empty<Variable>(), Enumerable.Empty<Calculation>());

			_calculator = A.Fake<ICalculator>();

			_runtime = new GraspRuntime(_schema, _calculator, Enumerable.Empty<VariableBinding>());
		}

		protected override void When()
		{
			_runtime.ApplyCalculations();
		}

		[Then]
		public void AppliesSelfToCalculator()
		{
			A.CallTo(() => _calculator.ApplyCalculation(_runtime)).MustHaveHappened(Repeated.Exactly.Once);
		}
	}
}