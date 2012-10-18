using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cloak;
using FluentAssertions;
using Xbehave;

namespace Grasp.Hypermedia
{
	public class HtmlRepresentations
	{
		[Scenario]
		public void CreateMRepresentation(MHead head, MContent body, MRepresentation representation)
		{
			"Given a header".Given(() => head = new MHead(new MText("Test"), new HtmlLink(new Uri("http://localhost"))));
			"And a body".And(() => body = new MText("Body"));

			"When creating its representation".When(() => representation = new MRepresentation(head, body));

			"It has the specified header".Then(() => representation.Head.Should().Be(head));
			"It has the specified body".Then(() => representation.Body.Should().Be(body));
		}

		[Scenario]
		public void GetMTextHtml(string value, MText text, object html)
		{
			"Given a value".Given(() => value = "Test");
			"And a text node".And(() => text = new MText(value));

			"When getting its HTML content".When(() => html = text.GetHtmlContent());

			"It has the specified value".Then(() => html.Should().Be(value));
		}

		[Scenario]
		public void GetMTextHtmlWithClass(string value, string className, MClass @class, MText text, object html)
		{
			"Given a value".Given(() => value = "Test");
			"And a class name".And(() => className = "c");
			"And a class node with that name".And(() => @class = new MClass(className));
			"And a text node with that value and class".And(() => text = new MText(value, @class));

			"When getting its HTML content".When(() => html = text.GetHtmlContent());

			var element = default(XElement);

			"It is an XML element".Then(() =>
			{
				html.Should().BeAssignableTo<XElement>();

				element = (XElement) html;
			});

			"It is named 'span'".Then(() => element.Name.Should().Be((XName) "span"));
			"It has the specified content".Then(() => element.Value.Should().Be(value));
			"It has a 'class' attribute".Then(() => element.Attribute("class").Should().NotBeNull());
			"The 'class' attribute has the specified class name".Then(() => element.Attribute("class").Value.Should().Be(className));
		}

		[Scenario]
		public void GetMLinkHtml(Uri href, HtmlLink htmlLink, MLink link, object html)
		{
			"Given an href".Given(() => href = new Uri("http://localhost"));
			"And an HTML link".And(() => htmlLink = new HtmlLink(href));
			"And a link node".And(() => link = new MLink(htmlLink));

			"When getting its HTML content".When(() => html = link.GetHtmlContent());

			var element = default(XElement);

			"It is an XML element".Then(() =>
			{
				html.Should().BeAssignableTo<XElement>();

				element = (XElement) html;
			});

			"It is named 'a'".Then(() => element.Name.Should().Be((XName) "a"));
			"It has the specified href".Then(() => element.Attribute("href").Value.Should().Be(href.ToString()));
		}

		[Scenario]
		public void GetMLinkHtmlWithClass(Uri href, HtmlLink htmlLink, string className, MClass @class, MLink link, object html)
		{
			"Given an href".Given(() => href = new Uri("http://localhost"));
			"And an HTML link".And(() => htmlLink = new HtmlLink(href));
			"And a class name".And(() => className = "c");
			"And a class node with that name".And(() => @class = new MClass(className));
			"And a link node with that HTML link and class".And(() => link = new MLink(htmlLink, @class));

			"When getting its HTML content".When(() => html = link.GetHtmlContent());

			var element = default(XElement);

			"It is an XML element".Then(() =>
			{
				html.Should().BeAssignableTo<XElement>();

				element = (XElement) html;
			});

			"It is named 'a'".Then(() => element.Name.Should().Be((XName) "a"));
			"It has the specified href".Then(() => element.Attribute("href").Value.Should().Be(href.ToString()));
			"It has a 'class' attribute".Then(() => element.Attribute("class").Should().NotBeNull());
			"The 'class' attribute has the specified class name".Then(() => element.Attribute("class").Value.Should().Be(className));
		}

		[Scenario]
		public void CreateMHead(MText title, HtmlLink baseLink, HtmlLink link1, HtmlLink link2, MHead head)
		{
			"Given a title".Given(() => title = new MText("Test"));
			"And a base link".And(() => baseLink = new HtmlLink(new Uri("http://localhost")));
			"And a link".And(() => link1 = new HtmlLink(new Uri("/1", UriKind.Relative)));
			"And another link".And(() => link2 = new HtmlLink(new Uri("/2", UriKind.Relative)));

			"When creating a header".When(() => head = new MHead(title, baseLink, link1, link2));

			"It has the specified title".Then(() => head.Title.Should().Be(title));
			"It has the specified base link".Then(() => head.BaseLink.Should().Be(baseLink));
			"It has both specified links in order".Then(() => head.Links.SequenceEqual(Params.Of(link1, link2)));
		}

		[Scenario]
		public void GetMHeadHtml(MText title, HtmlLink baseLink, HtmlLink link1, HtmlLink link2, MHead head, object html)
		{
			"Given a title".Given(() => title = new MText("Test"));
			"And a base link".And(() => baseLink = new HtmlLink(new Uri("http://localhost")));
			"And a link".And(() => link1 = new HtmlLink(new Uri("/1", UriKind.Relative)));
			"And another link".And(() => link2 = new HtmlLink(new Uri("/2", UriKind.Relative)));
			"And a header".And(() => head = new MHead(title, baseLink, link1, link2));

			"When getting its HTML content".When(() => html = head.GetHtmlContent());

			var element = default(XElement);

			"It is an XML element".Then(() =>
			{
				html.Should().BeAssignableTo<XElement>();

				element = (XElement) html;
			});

			"It is named 'head'".Then(() => element.Name.Should().Be((XName) "head"));
			"It has the specified title".Then(() => element.Element("title").Value.Should().Be(title.Value));
			"It has the base link".Then(() => element.Element("base").Should().NotBeNull());
			"It has both links".Then(() => element.Elements("link").Count().Should().Be(2));
		}
	}
}