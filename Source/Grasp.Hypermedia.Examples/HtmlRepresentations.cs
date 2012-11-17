using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cloak;
using FluentAssertions;
using Grasp.Hypermedia.Linq;
using Xunit;

namespace Grasp.Hypermedia
{
	public class HtmlRepresentations
	{
		[Fact] public void CreateMRepresentation()
		{
			var header = new MHeader("Title", new Hyperlink(""), new Hyperlink("", relationship: Relationship.Self));
			var body = new MCompositeContent();

			var representation = new MRepresentation(header, body);

			representation.Header.Should().Be(header);
			representation.Body.Should().Be(body);
		}

		[Fact] public void GetMTextHtml()
		{
			var text = new MValue("Test");

			var html = text.GetHtml();

			html.Should().Be("Test");
		}

		[Fact] public void GetMTextHtmlWithClass()
		{
			var text = new MValue("c", "Test");

			var html = text.GetHtml();

			html.Should().BeAssignableTo<XElement>();

			var element = (XElement) html;

			element.Name.Should().Be((XName) "span");
			element.Value.Should().Be("Test");
			element.Attribute("class").Should().NotBeNull();
			element.Attribute("class").Value.Should().Be("c");
		}

		[Fact] public void GetMLinkHtml()
		{
			var link = new MLink(new Hyperlink(""));

			var html = link.GetHtml();

			html.Should().BeAssignableTo<XElement>();

			var element = (XElement) html;

			element.Name.Should().Be((XName) "a");
			element.Attribute(Hyperlink.HrefAttributeName).Should().NotBeNull();
			element.Attribute(Hyperlink.HrefAttributeName).Value.Should().Be("");
		}

		[Fact] public void GetMLinkHtmlWithClass()
		{
			var link = new MLink(new Hyperlink(""), new MClass("c"));

			var html = link.GetHtml();

			html.Should().BeAssignableTo<XElement>();

			var element = (XElement) html;

			element.Name.Should().Be((XName) "a");
			element.Attribute("href").Value.Should().Be("");
			element.Attribute("class").Should().NotBeNull();
			element.Attribute("class").Value.Should().Be("c");
		}

		[Fact] public void CreateMHeader()
		{
			var title = "Title";
			var baseLink = new Hyperlink("");
			var selfLink = new Hyperlink("", relationship: Relationship.Self);
			var link1 = new Hyperlink("1");
			var link2 = new Hyperlink("2");

			var header = new MHeader(title, baseLink, selfLink, link1, link2);

			header.Title.Should().Be(title);
			header.BaseLink.Should().Be(baseLink);
			header.SelfLink.Should().Be(selfLink);
			header.Links.SequenceEqual(Params.Of(link1, link2));
		}

		[Fact] public void GetMHeaderHtml()
		{
			var baseLink = new Hyperlink("");
			var selfLink = new Hyperlink("", relationship: Relationship.Self);
			var link1 = new Hyperlink("");
			var link2 = new Hyperlink("");
			var header = new MHeader("Title", baseLink, selfLink, link1, link2);

			var html = header.GetHtml();

			html.Should().BeAssignableTo<XElement>();

			var element = (XElement) html;

			element.Name.Should().Be((XName) "head");
			element.Element("title").Value.Should().Be("Title");
			element.Element("base").Should().NotBeNull();
			element.Element("base").Value.Should().Be(baseLink.Uri.ToString());
			element.Elements("link").Count().Should().Be(3);
			element.Elements("link").ElementAt(0).Value.Should().Be(selfLink.Uri.ToString());
			element.Elements("link").ElementAt(1).Value.Should().Be(link1.Uri.ToString());
			element.Elements("link").ElementAt(2).Value.Should().Be(link2.Uri.ToString());
		}
	}
}