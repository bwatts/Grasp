using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp
{
	public class CreateVariableBinding : Behavior
	{
		Variable _variable;
		int _value;
		VariableBinding _binding;

		protected override void Given()
		{
			_variable = new Variable("Grasp", "Test", typeof(int));

			_value = 1;
		}

		protected override void When()
		{
			_binding = new VariableBinding(_variable, _value);
		}

		[Then]
		public void HasOriginalVariable()
		{
			Assert.That(_binding.Variable, Is.EqualTo(_variable));
		}

		[Then]
		public void HasOriginalValue()
		{
			Assert.That(_binding.Value, Is.EqualTo(_value));
		}
	}
}