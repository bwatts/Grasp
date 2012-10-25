using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FluentAssertions;
using Xunit;

namespace Grasp.Hypermedia
{
	public class HtmlLinks
	{
		[Fact] public void Get()
		{
			var link = new HtmlLink(Href.Root);

			var html = link.ToHtml("a");

			html.Name.Should().Be((XName) "a");
			html.Attribute("href").Should().NotBeNull();
			html.Attribute("href").Value.Should().Be(Href.Root.ToString());
		}

		[Fact] public void GetWithRelationship()
		{
			var link = new HtmlLink(Href.Root, relationship: Relationship.Self);

			var html = link.ToHtml("a");

			html.Attribute("rel").Should().NotBeNull();
			html.Attribute("rel").Value.Should().Be(Relationship.Self.Name);
		}

		[Fact] public void GetWithContent()
		{
			var link = new HtmlLink(Href.Root, content: 1);

			var html = link.ToHtml("a");

			html.Value.Should().Be("1");
		}

		[Fact] public void GetWithContentAndRelationship()
		{
			var link = new HtmlLink(Href.Root, 1, "Title", Relationship.Self);

			var html = link.ToHtml("a");

			html.Attribute("href").Should().NotBeNull();
			html.Attribute("href").Value.Should().Be(Href.Root.ToString());
			html.Attribute("rel").Should().NotBeNull();
			html.Attribute("rel").Value.Should().Be(Relationship.Self.Name);
			html.Value.Should().Be("1");
		}
	}
}