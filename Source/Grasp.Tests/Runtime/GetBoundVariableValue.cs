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
	public class GetBoundVariableValue : Behavior
	{
		Variable _variable;
		GraspSchema _schema;
		int _value;
		GraspRuntime _runtime;
		object _variableValue;

		protected override void Given()
		{
			_variable = new Variable("Grasp", "Test", typeof(int));

			_schema = new GraspSchema(new[] { _variable }, Enumerable.Empty<Calculation>());

			_value = 1;

			_runtime = new GraspRuntime(_schema, A.Fake<ICalculator>(), new[] { new VariableBinding(_variable, _value) });
		}

		protected override void When()
		{
			_variableValue = _runtime.GetVariableValue(_variable);
		}

		[Then]
		public void GetsBoundValue()
		{
			Assert.That(_variableValue, Is.EqualTo(_value));
		}
	}
}