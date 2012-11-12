using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cloak;
using Cloak.Xml;
using Grasp.Checks;

namespace Grasp.Hypermedia.Linq
{
	internal static class MRepresentationReader
	{
		internal static MRepresentation Read(XDocument xml)
		{
			xml.ValidateDocumentType();

			xml.Root.EnsureName("html");

			return new MRepresentation(xml.ReadHead(), xml.ReadBody());
		}

		private static void ValidateDocumentType(this XDocument xml)
		{
			if(Check.That(xml.DocumentType.Name).IsNotNullOrEmpty())
			{
				if(xml.DocumentType.Name != "html"
					|| Check.That(xml.DocumentType.PublicId).IsNotNullOrEmpty()
					|| Check.That(xml.DocumentType.SystemId).IsNotNullOrEmpty()
					|| Check.That(xml.DocumentType.InternalSubset).IsNotNullOrEmpty())
				{
					throw new FormatException(Resources.InvalidHtmlDocumentType);
				}
			}
		}

		private static MHead ReadHead(this XDocument xml)
		{
			var head = xml.Root.RequiredElement("head");

			return new MHead(
				head.RequiredElement("title").RequiredString(),
				head.RequiredElement("base").ReadHyperlink(),
				head.Elements("link").Select(linkElement => linkElement.ReadHyperlink()));
		}

		private static MContent ReadBody(this XDocument xml)
		{
			var body = xml.Root.RequiredElement("body");

			return body.Elements().ReadMContent();
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
				return new MValue(element.Value);
			}
		}

		private static Hyperlink ReadHyperlink(this XElement element)
		{
			var title = ((string) element.Attribute(Hyperlink.TitleAttributeName)) ?? "";
			var rel = ((string) element.Attribute(Hyperlink.RelAttributeName) ?? "");
			var href = element.RequiredAttribute(Hyperlink.HrefAttributeName).RequiredString();

			return new Hyperlink(href, element.Value, title, rel);
		}

		private static MLink ReadMLink(this XElement element)
		{
			return new MLink(element.ReadHyperlink(), element.ReadMClass());
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
			var y = itemElements.Select(itemElement => itemElement.Elements().ReadMContent()).ToList();

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