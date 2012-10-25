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

			@class.IsEmpty.Should().BeFalse();
			@class.Name.Should().Be("c");
		}

		[Fact] public void GetMClassEmpty()
		{
			var @class = MClass.Empty;

			@class.IsEmpty.Should().BeTrue();
			@class.Name.Should().BeEmpty();
		}
	}
}