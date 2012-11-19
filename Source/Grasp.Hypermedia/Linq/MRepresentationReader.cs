using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Cloak;
using Cloak.Xml;

namespace Grasp.Hypermedia.Linq
{
	internal static class MRepresentationReader
	{
		internal static MRepresentation Read(XDocument xml)
		{
			xml.Root.EnsureName("html");

			return new MRepresentation(xml.ReadHeader(), xml.ReadBody());
		}

		private static MHeader ReadHeader(this XDocument xml)
		{
			var head = xml.Root.RequiredElement("head");

			var links = head.Elements("link").Select(linkElement => linkElement.ReadHyperlink()).ToList();

			var selfLinkIndex = links.FindIndex(link => link.Relationship == Relationship.Self);

			if(selfLinkIndex == -1)
			{
				throw new FormatException(Resources.MissingSelfLink.FormatInvariant(Relationship.Self));
			}

			var selfLink = links[selfLinkIndex];

			links.RemoveAt(selfLinkIndex);

			return new MHeader(head.RequiredElement("title").RequiredString(), head.RequiredElement("base").ReadHyperlink(), selfLink, links);
		}

		private static MCompositeContent ReadBody(this XDocument xml)
		{
			var body = xml.Root.RequiredElement("body");

			return new MCompositeContent(body.Elements().ReadMContents());
		}

		private static MContent ReadMContent(this IEnumerable<XElement> elements)
		{
			var contents = elements.ReadMContents().ToList();

			return contents.Count == 1 ? contents.Single() : new MCompositeContent(contents);
		}

		private static IEnumerable<MContent> ReadMContents(this IEnumerable<XElement> elements)
		{
			return elements.Select(ReadMContent);
		}

		private static MContent ReadMContent(this XElement element)
		{
			if(element.NameIs("a"))
			{
				return element.ReadMLink();
			}
			else if(element.NameIs("span"))
			{
				return element.ReadMValue();
			}
			else if(element.NameIs("div"))
			{
				return element.ReadMDivision();
			}
			else if(element.NameIs("dl"))
			{
				return element.ReadMDefinitionList();
			}
			else if(element.NameIs("ul") || element.NameIs("ol"))
			{
				return element.ReadMList();
			}
			else
			{
				var escaped = element.DescendantNodes().Any(descendent => descendent.NodeType == XmlNodeType.CDATA);

				return new MValue((string) element, escaped);
			}
		}

		private static Hyperlink ReadHyperlink(this XElement element)
		{
			var title = ((string) element.Attribute(Hyperlink.TitleAttributeName)) ?? "";
			var rel = ((string) element.Attribute(Hyperlink.RelAttributeName) ?? "");
			var href = element.RequiredAttribute(Hyperlink.HrefAttributeName).RequiredString();
			var @class = element.ReadMClass();

			return new Hyperlink(href, element.Value, title, rel, @class);
		}

		private static MLink ReadMLink(this XElement element)
		{
			return new MLink(element.ReadHyperlink());
		}

		private static MClass ReadMClass(this XElement element)
		{
			return new MClass(((string) element.Attribute("class")) ?? "");
		}

		private static MValue ReadMValue(this XElement element)
		{
			return new MValue(element.ReadMClass(), element.Value);
		}

		private static MDivision ReadMDivision(this XElement element)
		{
			return new MDivision(element.ReadMClass(), element.Elements().ReadMContents());
		}

		private static MDefinitionList ReadMDefinitionList(this XElement element)
		{
			return new MDefinitionList(element.ReadMClass(), element.ReadDefinitions());
		}

		private static MList ReadMList(this XElement element)
		{
			return new MList(element.ReadMClass(), ReadMListItems(element.Elements("li")));
		}

		private static IEnumerable<MContent> ReadMListItems(IEnumerable<XElement> itemElements)
		{
			return itemElements.Select(itemElement => itemElement.Elements().ReadMContent());
		}

		private static IEnumerable<KeyValuePair<MValue, MContent>> ReadDefinitions(this XElement element)
		{
			MValue term = null;

			foreach(var childElement in element.Elements())
			{
				if(term == null && childElement.NameIsNot("dt"))
				{
					throw new FormatException(Resources.ExpectingTerm.FormatInvariant(element.GetPath()));
				}
				else if(term != null && childElement.NameIsNot("dd"))
				{
					throw new FormatException(Resources.ExpectingDefinition.FormatInvariant(element.GetPath()));
				}
				else if(term == null)
				{
					term = new MValue(element.ReadMClass(), childElement.Value);
				}
				else
				{
					yield return new KeyValuePair<MValue, MContent>(term, childElement.ReadMContent());

					term = null;
				}
			}

			if(term != null)
			{
				throw new FormatException(Resources.TermHasNoDefinition.FormatInvariant(term));
			}
		}

		private static bool NameIs(this XElement element, string name)
		{
			return element.Name.ToString().Equals(name, StringComparison.InvariantCultureIgnoreCase);
		}

		private static bool NameIsNot(this XElement element, string name)
		{
			return !element.NameIs(name);
		}
	}
}