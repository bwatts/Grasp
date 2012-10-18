using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FluentAssertions;
using Xbehave;

namespace Grasp.Hypermedia
{
	public class HtmlLinks
	{
		[Scenario]
		public void Get(XName elementName, Uri href, HtmlLink link, XElement html)
		{
			"Given an element name".Given(() => elementName = "a");
			"And an href".And(() => href = new Uri("http://localhost"));
			"And a link".And(() => link = new HtmlLink(href));

			"When getting the HTML".When(() => html = link.ToHtml(elementName));

			"It has the specified element name".Then(() => html.Name.Should().Be(elementName));
			"It has an href attribute".Then(() => html.Attribute("href").Should().NotBeNull());
			"It has the specified href".Then(() => html.Attribute("href").Value.Should().Be(href.ToString()));
		}

		[Scenario]
		public void GetWithRelationship(Relationship relationship, HtmlLink link, XElement html)
		{
			"Given a relationship".Given(() => relationship = Relationship.Default);
			"And a link with that relationship".And(() => link = new HtmlLink(new Uri("http://localhost"), relationship));

			"When getting the HTML".When(() => html = link.ToHtml("a"));

			"It has a relationship attribute".Then(() => html.Attribute("rel").Should().NotBeNull());
			"It has the specified relationship".Then(() => html.Attribute("rel").Value.Should().Be(relationship.Name));
		}

		[Scenario]
		public void GetWithTarget(string target, HtmlLink link, XElement html)
		{
			"Given a target".Given(() => target = "_self");
			"And a link with that target".And(() => link = new HtmlLink(new Uri("http://localhost"), target: target));

			"When getting the HTML".When(() => html = link.ToHtml("a"));

			"It has a target attribute".Then(() => html.Attribute("target").Should().NotBeNull());
			"It has the specified target".Then(() => html.Attribute("target").Value.Should().Be(target));
		}

		[Scenario]
		public void GetWithContent(int content, HtmlLink link, XElement html)
		{
			"Given some content".Given(() => content = 1);
			"And a link with that content".And(() => link = new HtmlLink(new Uri("http://localhost"), content: content));

			"When getting the HTML".When(() => html = link.ToHtml("a"));

			"It has the specified content".Then(() => html.Value.Should().Be("1"));
		}

		[Scenario]
		public void GetRelationshipTargetAndContent(XName elementName, Uri href, Relationship relationship, string target, int content, HtmlLink link, XElement html)
		{
			"Given an element name".Given(() => elementName = "a");
			"And an href".And(() => href = new Uri("http://localhost"));
			"And a relationship".Given(() => relationship = Relationship.Default);
			"And a target".Given(() => target = "_self");
			"And some content".And(() => content = 1);
			"And a link with that href, relationship, target, and content".And(() => link = new HtmlLink(href, relationship, target, content));

			"When getting the HTML".When(() => html = link.ToHtml("a"));

			"It has an href attribute".Then(() => html.Attribute("href").Should().NotBeNull());
			"It has the specified href".Then(() => html.Attribute("href").Value.Should().Be(href.ToString()));
			"It has a relationship attribute".Then(() => html.Attribute("rel").Should().NotBeNull());
			"It has the specified relationship".Then(() => html.Attribute("rel").Value.Should().Be(relationship.Name));
			"It has a target attribute".Then(() => html.Attribute("target").Should().NotBeNull());
			"It has the specified target".Then(() => html.Attribute("target").Value.Should().Be(target));
			"It has the specified content".Then(() => html.Value.Should().Be("1"));
		}
	}
}