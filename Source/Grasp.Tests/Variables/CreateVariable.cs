using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.NUnit;
using NUnit.Framework;

namespace Grasp.Variables
{
	public class CreateVariable : Behavior
	{
		string _namespace;
		string _name;
		Type _type;
		Variable _variable;

		protected override void Given()
		{
			_namespace = "Grasp";
			_name = "Test";
			_type = typeof(int);
		}

		protected override void When()
		{
			_variable = new Variable(_namespace, _name, _type);
		}

		[Then]
		public void HasOriginalNamespace()
		{
			Assert.That(_variable.Namespace, Is.EqualTo(_namespace));
		}

		[Then]
		public void HasOriginalName()
		{
			Assert.That(_variable.Name, Is.EqualTo(_name));
		}

		[Then]
		public void HasOriginalType()
		{
			Assert.That(_variable.Type, Is.EqualTo(_type));
		}
	}
}