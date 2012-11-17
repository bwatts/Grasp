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
			var link = new Hyperlink("");

			var html = link.ToHtml("a");

			html.Name.Should().Be((XName) "a");
			html.Attribute("href").Should().NotBeNull();
			html.Attribute("href").Value.Should().Be("");
		}

		[Fact] public void GetWithRelationship()
		{
			var link = new Hyperlink("", relationship: Relationship.Self);

			var html = link.ToHtml("a");

			html.Attribute("rel").Should().NotBeNull();
			html.Attribute("rel").Value.Should().Be(Relationship.Self.Value);
		}

		[Fact] public void GetWithContent()
		{
			var link = new Hyperlink("", content: 1);

			var html = link.ToHtml("a");

			html.Value.Should().Be("1");
		}

		[Fact] public void GetWithContentAndRelationship()
		{
			var link = new Hyperlink("", 1, "Title", Relationship.Self);

			var html = link.ToHtml("a");

			html.Attribute("href").Should().NotBeNull();
			html.Attribute("href").Value.Should().Be("");
			html.Attribute("rel").Should().NotBeNull();
			html.Attribute("rel").Value.Should().Be(Relationship.Self.Value);
			html.Value.Should().Be("1");
		}
	}
}