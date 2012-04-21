using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using FakeItEasy;
using NUnit.Framework;

namespace Grasp.Analysis.Compilation
{
	public class GetRuntime : Behavior
	{
		Variable _variable;
		GraspSchema _schema;
		GraspExecutable _executable;
		int _initialValue;
		IRuntimeSnapshot _initialState;
		GraspRuntime _runtime;

		protected override void Given()
		{
			_variable = new Variable("Grasp", "Test", typeof(int));

			_schema = new GraspSchema(new[] { _variable }, Enumerable.Empty<Calculation>());

			_initialState = A.Fake<IRuntimeSnapshot>();

			_initialValue = 1;

			A.CallTo(() => _initialState.GetValue(_variable)).Returns(_initialValue);

			_executable = new GraspExecutable(_schema, A.Fake<ICalculator>());
		}

		protected override void When()
		{
			_runtime = _executable.GetRuntime(_initialState);
		}

		[Then]
		public void BindsVariableToInitialValue()
		{
			Assert.That(_runtime.GetVariableValue(_variable), Is.EqualTo(_initialValue));
		}
	}
}