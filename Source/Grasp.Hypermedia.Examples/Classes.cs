using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Grasp.Hypermedia.Linq;
using Xunit;

namespace Grasp.Hypermedia
{
	public class Classes
	{
		[Fact] public void CreateMClass()
		{
			var @class = new MClass("c");

			@class.Value.Should().Be("c");
		}

		[Fact] public void GetMClassEmpty()
		{
			var @class = MClass.Empty;

			@class.Value.Should().BeEmpty();
		}
	}
}