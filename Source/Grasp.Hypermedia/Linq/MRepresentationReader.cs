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
			if(element.Name == "a")
			{
				return element.ReadLink();
			}
			else if(element.Name == "span")
			{
				return element.ReadValue();
			}
			else if(element.Name == "section")
			{
				return element.ReadSection();
			}
			else if(element.Name == "dl")
			{
				return element.ReadDescriptionList();
			}
			else if(element.Name == "ul" || element.Name == "ol")
			{
				return element.ReadList();
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
			var @class = element.ReadClass();

			return new Hyperlink(href, element.Value, title, rel, @class);
		}

		private static MLink ReadLink(this XElement element)
		{
			return new MLink(element.ReadHyperlink());
		}

		private static MClass ReadClass(this XElement element)
		{
			return new MClass(((string) element.Attribute("class")) ?? "");
		}

		private static MValue ReadValue(this XElement element)
		{
			return new MValue(element.ReadClass(), element.Value);
		}

		private static MSection ReadSection(this XElement element)
		{
			return new MSection(element.ReadClass(), element.Elements().ReadMContents());
		}

		private static MDescriptionList ReadDescriptionList(this XElement element)
		{
			return new MDescriptionList(element.ReadClass(), element.ReadDescriptions());
		}

		private static MList ReadList(this XElement element)
		{
			return new MList(ReadListItems(element.Elements("li")));
		}

		private static IEnumerable<MContent> ReadListItems(IEnumerable<XElement> itemElements)
		{
			return itemElements.Select(itemElement => itemElement.Elements().ReadMContent());
		}

		private static IEnumerable<MDescriptionItem> ReadDescriptions(this XElement element)
		{
			var terms = new List<MValue>();
			var descriptions = new List<MContent>();

			var foundTerm = false;
			var priorWasDescription = false;

			foreach(var childElement in element.Elements())
			{
				if(childElement.Name == "dt")
				{
					if(priorWasDescription)
					{
						yield return new MDescriptionItem(terms, descriptions);

						terms = new List<MValue>();
						descriptions = new List<MContent>();
					}

					terms.Add(childElement.ReadValue());

					foundTerm = true;
					priorWasDescription = false;
				}
				else
				{
					if(childElement.Name == "dd")
					{
						if(!foundTerm)
						{
							throw new FormatException(Resources.FoundDescriptionBeforeTerms);
						}

						descriptions.Add(childElement.ReadMContent());

						priorWasDescription = true;
					}
				}
			}

			if(terms.Any())
			{
				if(!descriptions.Any())
				{
					throw new FormatException(Resources.FoundTermsWithoutDescription);
				}

				yield return new MDescriptionItem(terms, descriptions);
			}
		}
	}
}