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
	public class SetUnboundVariableValue : Behavior
	{
		Variable _variable;
		GraspSchema _schema;
		GraspRuntime _runtime;
		int _value;
		object _boundValue;

		protected override void Given()
		{
			_variable = new Variable("Grasp", "Test", typeof(int));

			_schema = new GraspSchema(new[] { _variable }, Enumerable.Empty<Calculation>());

			_runtime = new GraspRuntime(_schema, A.Fake<ICalculator>(), Enumerable.Empty<VariableBinding>());

			_value = 2;
		}

		protected override void When()
		{
			_runtime.SetVariableValue(_variable, _value);

			_boundValue = _runtime.GetVariableValue(_variable);
		}

		[Then]
		public void VariableIsBound()
		{
			Assert.That(_boundValue, Is.EqualTo(_value));
		}
	}
}