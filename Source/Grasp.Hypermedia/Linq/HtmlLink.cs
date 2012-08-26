using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	internal static class HtmlLink
	{
		internal static XElement ToHtmlLink(this HttpLink link, string elementName)
		{
			return new XElement(elementName, GetContent(link));
		}

		private static IEnumerable<object> GetContent(HttpLink link)
		{
			if(!String.IsNullOrEmpty(link.Relationship))
			{
				yield return new XAttribute("rel", link.Relationship);
			}

			yield return new XAttribute("href", link.Href);

			if(!String.IsNullOrEmpty(link.Target))
			{
				yield return new XAttribute("target", link.Target);
			}

			if(!String.IsNullOrEmpty(link.Text))
			{
				yield return link.Text;
			}
		}
	}
}