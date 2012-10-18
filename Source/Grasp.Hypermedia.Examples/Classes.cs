using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentAssertions;
using Xbehave;

namespace Grasp.Hypermedia
{
	public class Classes
	{
		[Scenario]
		public void CreateMClass(string name, MClass @class)
		{
			"Given a class name".Given(() => name = "c");

			"When creating a class node".When(() => @class = new MClass(name));

			"It is not none".Then(() => @class.IsNone.Should().BeFalse());
			"It has the specified class name".Then(() => @class.Name.Should().Be(name));
		}

		[Scenario]
		public void GetMClassNone(MClass @class)
		{
			"When getting the none class".When(() => @class = MClass.None);

			"It is none".Then(() => @class.IsNone.Should().BeTrue());
			"Its name is empty".Then(() => @class.Name.Should().BeEmpty());
		}
	}
}