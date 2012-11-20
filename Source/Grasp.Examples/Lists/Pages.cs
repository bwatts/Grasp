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
	public class Pages
	{
		[Fact] public void Empty()
		{
			var pages = new ListViewPages(ListViewKey.Empty, Count.None);

			pages.Count.Should().Be(Count.None);
			pages.Current.Should().Be(Count.None);
			pages.Previous.Should().Be(Count.None);
			pages.Next.Should().Be(Count.None);
		}

		[Fact] public void StartOfSinglePage()
		{
			var key = new ListViewKey(start: new Count(1), size: new Count(10));
			var totalItemCount = new Count(10);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(1));
			pages.Current.Should().Be(new Count(1));
			pages.Previous.Should().Be(Count.None);
			pages.Next.Should().Be(Count.None);
		}

		[Fact] public void StartOfMiddlePage()
		{
			var key = new ListViewKey(start: new Count(11), size: new Count(10));
			var totalItemCount = new Count(30);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(3));
			pages.Current.Should().Be(new Count(2));
			pages.Previous.Should().Be(new Count(1));
			pages.Next.Should().Be(new Count(3));
		}

		[Fact] public void StartOfLastPage()
		{
			var key = new ListViewKey(start: new Count(11), size: new Count(10));
			var totalItemCount = new Count(20);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(2));
			pages.Current.Should().Be(new Count(2));
			pages.Previous.Should().Be(new Count(1));
			pages.Next.Should().Be(Count.None);
		}

		[Fact] public void MiddleOfSinglePage()
		{
			var key = new ListViewKey(start: new Count(5), size: new Count(10));
			var totalItemCount = new Count(10);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(1));
			pages.Current.Should().Be(new Count(1));
			pages.Previous.Should().Be(Count.None);
			pages.Next.Should().Be(Count.None);
		}

		[Fact] public void CreateInMiddleOfMiddlePage()
		{
			var key = new ListViewKey(start: new Count(15), size: new Count(10));
			var totalItemCount = new Count(30);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(3));
			pages.Current.Should().Be(new Count(2));
			pages.Previous.Should().Be(new Count(1));
			pages.Next.Should().Be(new Count(3));
		}

		[Fact] public void CreateInMiddleOfLastPage()
		{
			var key = new ListViewKey(start: new Count(15), size: new Count(10));
			var totalItemCount = new Count(20);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(2));
			pages.Current.Should().Be(new Count(2));
			pages.Previous.Should().Be(new Count(1));
			pages.Next.Should().Be(Count.None);
		}

		[Fact] public void EndOfSinglePage()
		{
			var key = new ListViewKey(start: new Count(10), size: new Count(10));
			var totalItemCount = new Count(10);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(1));
			pages.Current.Should().Be(new Count(1));
			pages.Previous.Should().Be(Count.None);
			pages.Next.Should().Be(Count.None);
		}

		[Fact] public void EndOfMiddlePage()
		{
			var key = new ListViewKey(start: new Count(20), size: new Count(10));
			var totalItemCount = new Count(30);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(3));
			pages.Current.Should().Be(new Count(2));
			pages.Previous.Should().Be(new Count(1));
			pages.Next.Should().Be(new Count(3));
		}

		[Fact] public void EndOfLastPage()
		{
			var key = new ListViewKey(start: new Count(20), size: new Count(10));
			var totalItemCount = new Count(20);

			var pages = new ListViewPages(key, totalItemCount);

			pages.Count.Should().Be(new Count(2));
			pages.Current.Should().Be(new Count(2));
			pages.Previous.Should().Be(new Count(1));
			pages.Next.Should().Be(Count.None);
		}
	}
}