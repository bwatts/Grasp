using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis.Variables
{
	public class Variables
	{
		[Scenario]
		public void CreateWithNamespaceAndName(string @namespace, string name, Type type, Variable variable)
		{
			"Given a namespace".Given(() => @namespace = "Grasp");
			"And a name".And(() => name = "X");
			"And a type".Given(() => type = typeof(int));

			"When creating a variable".When(() => variable = new Variable(type, @namespace, name));

			"It has the specified namespace".Then(() => variable.Namespace.Should().Be(@namespace));
			"It has the specified name".Then(() => variable.Name.Should().Be(name));
			"It has the specified type".Then(() => variable.Type.Should().Be(type));
		}

		[Scenario]
		public void CreateWithFullName(string fullName, Type type, Variable variable)
		{
			"Given a full name".Given(() => fullName = "Grasp.X");
			"And a type".Given(() => type = typeof(int));

			"When creating a variable".When(() => variable = new Variable(type, fullName));

			"It has the specified namespace".Then(() => variable.Namespace.Should().Be("Grasp"));
			"It has the specified name".Then(() => variable.Name.Should().Be("X"));
			"It has the specified type".Then(() => variable.Type.Should().Be(type));
		}

		[Scenario]
		public void GetText(string fullName, Variable variable, string text)
		{
			"Given a full name".Given(() => fullName = "Grasp.X");
			"And a variable".And(() => variable = new Variable<int>(fullName));

			"When calling .ToString() on the variable".When(() => text = variable.ToString());

			"The text is the specified full name".Then(() => variable.ToString().Should().Be(fullName));
		}
	}
}