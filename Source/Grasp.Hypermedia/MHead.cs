using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia
{
	public sealed class MHead : Notion
	{
		public static Field<MText> TitleField = Field.On<MHead>.Backing(x => x.Title);
		public static Field<HtmlLink> BaseLinkField = Field.On<MHead>.Backing(x => x.BaseLink);
		public static Field<Many<HtmlLink>> LinksField = Field.On<MHead>.Backing(x => x.Links);

		public MHead(MText title, HtmlLink baseLink, IEnumerable<HtmlLink> links)
		{
			Contract.Requires(title != null);
			Contract.Requires(baseLink != null);
			Contract.Requires(links != null);

			Title = title;
			BaseLink = baseLink;
			Links = new Many<HtmlLink>(links);
		}

		public MHead(MText title, HtmlLink baseLink, params HtmlLink[] links) : this(title, baseLink, links as IEnumerable<HtmlLink>)
		{}

		public MText Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public HtmlLink BaseLink { get { return GetValue(BaseLinkField); } private set { SetValue(BaseLinkField, value); } }
		public Many<HtmlLink> Links { get { return GetValue(LinksField); } private set { SetValue(LinksField, value); } }

		internal object GetHtmlContent()
		{
			return new XElement("head", GetHeadContent());
		}

		private IEnumerable<XElement> GetHeadContent()
		{
			yield return new XElement("title", Title.Value);

			yield return BaseLink.ToHtml("base");

			foreach(var link in Links)
			{
				yield return link.ToHtml("link");
			}
		}
	}
}