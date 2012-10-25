using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Hypermedia
{
	public class HrefQueries
	{
		[Fact] public void Empty()
		{
			var query = HrefQuery.Empty;

			query.Value.Should().Be("");
			query.Count.Should().Be(0);
		}

		[Fact] public void Parameter()
		{
			var query = new HrefQuery("a=1");

			query.Value.Should().Be("a=1");
			query.Count.Should().Be(1);

			var part = query.Single();

			part.Key.Should().Be(new HrefPart("a"));
			part.Should().ContainInOrder(new HrefPart("1"));
		}

		[Fact] public void ParameterWithMultipleValues()
		{
			var query = new HrefQuery("a=1&a=2");

			query.Value.Should().Be("a=1&a=2");
			query.Count.Should().Be(1);

			var part = query.Single();

			part.Key.Should().Be(new HrefPart("a"));
			part.Should().ContainInOrder(new HrefPart("1"), new HrefPart("2"));
		}

		[Fact] public void MultipleParameters()
		{
			var query = new HrefQuery("a=1&b=2");

			query.Value.Should().Be("a=1&b=2");
			query.Count.Should().Be(2);

			var a = query.ElementAt(0);

			a.Key.Should().Be(new HrefPart("a"));
			a.Should().ContainInOrder(new HrefPart("1"));

			var b = query.ElementAt(1);

			b.Key.Should().Be(new HrefPart("b"));
			b.Should().ContainInOrder(new HrefPart("2"));
		}

		[Fact] public void MultipleParametersWithMultipleValues()
		{
			var query = new HrefQuery("a=1&b=2&a=3&b=4");

			query.Value.Should().Be("a=1&a=3&b=2&b=4");
			query.Count.Should().Be(2);

			var a = query.ElementAt(0);

			a.Key.Should().Be(new HrefPart("a"));
			a.Should().ContainInOrder(new HrefPart("1"), new HrefPart("3"));

			var b = query.ElementAt(1);

			b.Key.Should().Be(new HrefPart("b"));
			b.Should().ContainInOrder(new HrefPart("2"), new HrefPart("4"));
		}

		[Fact] public void EscapedValue()
		{
			var query = new HrefQuery("a=1 2");

			query.Value.Should().Be("a=1%202");
			query.Count.Should().Be(1);

			var a = query.ElementAt(0);

			a.Key.Should().Be(new HrefPart("a"));
			a.Single().Value.Should().Be("1 2");
		}

		[Fact] public void StartingWithSeparator()
		{
			var query = new HrefQuery("?a=1");

			query.Value.Should().Be("a=1");
			query.Count.Should().Be(1);

			var a = query.ElementAt(0);

			a.Key.Should().Be(new HrefPart("a"));
			a.Should().ContainInOrder(new HrefPart("1"));
		}

		[Fact] public void WithPath()
		{
			var query = new HrefQuery("/a/b/c?d=1");

			query.Value.Should().Be("d=1");
			query.Count.Should().Be(1);

			var a = query.ElementAt(0);

			a.Key.Should().Be(new HrefPart("d"));
			a.Should().ContainInOrder(new HrefPart("1"));
		}

		[Fact] public void WithoutValue()
		{
			var query = new HrefQuery("?a");

			query.Value.Should().Be("a");
			query.Count.Should().Be(1);

			var a = query.ElementAt(0);

			a.Key.Should().Be(new HrefPart("a"));
			a.Should().ContainInOrder(new HrefPart(""));
		}

		[Fact] public void RepeatedSeparator()
		{
			Assert.Throws<FormatException>(() => new HrefQuery("?a=?1"));
		}

		[Fact] public void BindParameter()
		{
			var query = new HrefQuery("a={a}");

			var boundQuery = query.BindParameter("a", "1");

			boundQuery.Value.Should().Be("a=1");
		}

		[Fact] public void BindNonParameter()
		{
			var query = new HrefQuery("a=1");

			var boundQuery = query.BindParameter("a", "2");

			boundQuery.Value.Should().Be("a=1");
		}
	}
}