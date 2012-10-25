using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MHead : Notion
	{
		public static readonly Field<string> TitleField = Field.On<MHead>.For(x => x.Title);
		public static readonly Field<HtmlLink> BaseLinkField = Field.On<MHead>.For(x => x.BaseLink);
		public static readonly Field<Many<HtmlLink>> LinksField = Field.On<MHead>.For(x => x.Links);

		public MHead(string title, HtmlLink baseLink, IEnumerable<HtmlLink> links)
		{
			Contract.Requires(title != null);
			Contract.Requires(baseLink != null);
			Contract.Requires(links != null);

			Title = title;
			BaseLink = baseLink;
			Links = new Many<HtmlLink>(links);
		}

		public MHead(string title, HtmlLink baseLink, params HtmlLink[] links) : this(title, baseLink, links as IEnumerable<HtmlLink>)
		{}

		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public HtmlLink BaseLink { get { return GetValue(BaseLinkField); } private set { SetValue(BaseLinkField, value); } }
		public Many<HtmlLink> Links { get { return GetValue(LinksField); } private set { SetValue(LinksField, value); } }

		internal object GetHtml()
		{
			return new XElement("head", GetHeadContent());
		}

		private IEnumerable<XElement> GetHeadContent()
		{
			yield return new XElement("title", Title);

			yield return BaseLink.ToHtml("base");

			foreach(var link in Links)
			{
				yield return link.ToHtml("link");
			}
		}
	}
}