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
	public class SetBoundVariableValue : Behavior
	{
		Variable _variable;
		GraspSchema _schema;
		VariableBinding _binding;
		GraspRuntime _runtime;
		int _newValue;

		protected override void Given()
		{
			_variable = new Variable("Grasp", "Test", typeof(int));

			_schema = new GraspSchema(new[] { _variable }, Enumerable.Empty<Calculation>());

			_binding = new VariableBinding(_variable, 1);

			_runtime = new GraspRuntime(_schema, A.Fake<ICalculator>(), new[] { _binding });

			_newValue = 2;
		}

		protected override void When()
		{
			_runtime.SetVariableValue(_variable, _newValue);
		}

		[Then]
		public void BindingIsUpdated()
		{
			Assert.That(_binding.Value, Is.EqualTo(_newValue));
		}
	}
}