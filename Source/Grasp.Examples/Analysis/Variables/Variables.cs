using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Analysis.Variables
{
	public class Variables
	{
		[Fact] public void CreateWithNamespaceAndName()
		{
			var @namespace = "Grasp";
			var name = "X";
			var type = typeof(int);

			var variable = new Variable(type, @namespace, name);

			variable.Namespace.Should().Be(@namespace);
			variable.Name.Should().Be(name);
			variable.Type.Should().Be(type);
		}

		[Fact] public void CreateWithFullName()
		{
			var fullName = "Grasp.X";
			var type = typeof(int);

			var variable = new Variable(type, fullName);

			variable.Namespace.Should().Be("Grasp");
			variable.Name.Should().Be("X");
			variable.Type.Should().Be(type);
		}

		[Fact] public void GetText()
		{
			var fullName = "Grasp.X";
			var variable = new Variable<int>(fullName);

			var text = variable.ToString();

			variable.ToString().Should().Be(fullName);
		}
	}
}