using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Analysis.Variables
{
	public class VariableToString : Behavior
	{
		Variable _variable;
		string _text;

		protected override void Given()
		{
			_variable = new Variable("TestNamespace", "Test", typeof(int));
		}

		protected override void When()
		{
			_text = _variable.ToString();
		}

		[Then]
		public void IsFullyQualified()
		{
			Assert.That(_text, Is.EqualTo("TestNamespace.Test"));
		}
	}
}