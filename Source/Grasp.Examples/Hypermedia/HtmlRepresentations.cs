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
			var head = new MHead("Title", new HtmlLink(Href.Root));
			var body = new MValue("Body");

			var representation = new MRepresentation(head, body);

			representation.Head.Should().Be(head);
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
			var text = new MValue("Test", new MClass("c"));

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
			var link = new MLink(new HtmlLink(Href.Root));

			var html = link.GetHtml();

			html.Should().BeAssignableTo<XElement>();

			var element = (XElement) html;

			element.Name.Should().Be((XName) "a");
			element.Attribute(HtmlLink.HrefAttributeName).Should().NotBeNull();
			element.Attribute(HtmlLink.HrefAttributeName).Value.Should().Be(Href.Root.ToString());
		}

		[Fact]
		public void GetMLinkHtmlWithClass()
		{
			var link = new MLink(new HtmlLink(Href.Root), new MClass("c"));

			var html = link.GetHtml();

			html.Should().BeAssignableTo<XElement>();

			var element = (XElement) html;

			element.Name.Should().Be((XName) "a");
			element.Attribute("href").Value.Should().Be(Href.Root.ToString());
			element.Attribute("class").Should().NotBeNull();
			element.Attribute("class").Value.Should().Be("c");
		}

		[Fact] public void CreateMHead()
		{
			var title = "Title";
			var baseLink = new HtmlLink(Href.Root);
			var link1 = new HtmlLink(new Href("1"));
			var link2 = new HtmlLink(new Href("2"));

			var head = new MHead(title, baseLink, link1, link2);

			head.Title.Should().Be(title);
			head.BaseLink.Should().Be(baseLink);
			head.Links.SequenceEqual(Params.Of(link1, link2));
		}

		[Fact] public void GetMHeadHtml()
		{
			var baseLink = new HtmlLink(Href.Root);
			var link1 = new HtmlLink(Href.Root);
			var link2 = new HtmlLink(Href.Root);
			var head = new MHead("Title", baseLink, link1, link2);

			var html = head.GetHtml();

			html.Should().BeAssignableTo<XElement>();

			var element = (XElement) html;

			element.Name.Should().Be((XName) "head");
			element.Element("title").Value.Should().Be("Title");
			element.Element("base").Should().NotBeNull();
			element.Element("base").Value.Should().Be(baseLink.Href.ToString());
			element.Elements("link").Count().Should().Be(2);
			element.Elements("link").ElementAt(0).Value.Should().Be(link1.Href.ToString());
			element.Elements("link").ElementAt(1).Value.Should().Be(link2.Href.ToString());
		}
	}
}