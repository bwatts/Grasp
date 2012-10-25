using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xbehave;
using Xunit;

namespace Grasp.Hypermedia
{
	public class RootHref
	{
		[Fact] public void Get()
		{
			var root = Href.Root;

			root.Parts.Should().BeEmpty();
			(root.Query as object).Should().Be(HrefQuery.Empty);
			root.IsTemplate.Should().BeFalse();
			root.Value.Should().Be("");
		}

		[Fact] public void ThenSeparator()
		{
			var href = Href.Root.Then(HrefPart.Separator);

			href.Should().Be(Href.Root);
		}

		[Fact] public void ThenPart()
		{
			var href = Href.Root.Then(new HrefPart("next"));

			href.Parts.Should().HaveCount(1);
			href.Parts.Single().Value.Should().Be("next");
			href.IsTemplate.Should().BeFalse();
			href.Value.Should().Be("next");
		}

		[Fact] public void ThenText()
		{
			var href = Href.Root.Then("next");

			href.Parts.Should().HaveCount(1);
			href.Parts.Single().Value.Should().Be("next");
			href.IsTemplate.Should().BeFalse();
			href.Value.Should().Be("next");
		}

		[Fact] public void ThenParameter()
		{
			var href = Href.Root.ThenParameter("a");

			href.Parts.Should().HaveCount(1);
			href.Parts.Single().Value.Should().Be("{a}");
			href.IsTemplate.Should().BeTrue();
			href.Value.Should().Be("{a}");
		}

		[Fact] public void ThenObject()
		{
			var href = Href.Root.Then(1);

			href.Parts.Should().HaveCount(1);
			href.Parts.Single().Value.Should().Be("1");
			href.IsTemplate.Should().BeFalse();
			href.Value.Should().Be("1");
		}

		[Fact] public void ThenFormattedObject()
		{
			var formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();
			formatInfo.NegativeSign = "NEGATIVE";

			var href = Href.Root.Then(-1, formatInfo);

			href.Parts.Should().HaveCount(1);
			href.Parts.Single().Value.Should().Be("NEGATIVE1");
			href.IsTemplate.Should().BeFalse();
			href.Value.Should().Be("NEGATIVE1");
		}

		[Fact] public void ToRelativeUri()
		{
			var href = Href.Root.ToUri();

			href.ToString().Should().Be("");
		}

		[Fact] public void ToAbsoluteUri()
		{
			var href = Href.Root.ToAbsoluteUri(new Uri("http://localhost"));

			href.ToString().Should().Be("http://localhost/");
		}
	}
}