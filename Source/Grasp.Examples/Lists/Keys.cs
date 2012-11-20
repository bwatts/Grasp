using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FluentAssertions;
using Xunit;

namespace Grasp.Lists
{
	public class Keys
	{
		[Fact] public void Create()
		{
			var start = new Count(1);
			var size = new Count(1);
			var sort = new Sort(Enumerable.Empty<SortField>());

			var key = new ListViewKey(start, size, sort);

			key.Start.Should().Be(start);
			key.Size.Should().Be(size);
			key.Sort.Should().Be(sort);
		}

		[Fact] public void Empty()
		{
			var empty = ListViewKey.Empty;

			empty.Start.Should().Be(Count.None);
			empty.Size.Should().Be(Count.None);
			empty.Sort.Should().Be(Sort.Empty);
		}

		[Fact] public void End()
		{
			var key = new ListViewKey(start: new Count(11), size: new Count(10));

			key.End.Should().Be(new Count(20));
		}
	}
}