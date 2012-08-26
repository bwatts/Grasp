using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Grasp.Knowledge;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MHead : Notion
	{
		public static Field<MText> TitleField = Field.On<MHead>.Backing(x => x.Title);
		public static Field<HttpLink> BaseLinkField = Field.On<MHead>.Backing(x => x.BaseLink);
		public static Field<Many<HttpLink>> LinksField = Field.On<MHead>.Backing(x => x.Links);

		public MHead(MText title, HttpLink baseLink, IEnumerable<HttpLink> links)
		{
			Contract.Requires(title != null);
			Contract.Requires(baseLink != null);
			Contract.Requires(links != null);

			Title = title;
			BaseLink = baseLink;
			Links = new Many<HttpLink>(links);
		}

		public MHead(MText title, HttpLink baseLink, params HttpLink[] links) : this(title, baseLink, links as IEnumerable<HttpLink>)
		{}

		public MText Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public HttpLink BaseLink { get { return GetValue(BaseLinkField); } private set { SetValue(BaseLinkField, value); } }
		public Many<HttpLink> Links { get { return GetValue(LinksField); } private set { SetValue(LinksField, value); } }

		internal object GetHtmlContent()
		{
			return new XElement("head", GetHeadContent());
		}

		private IEnumerable<XElement> GetHeadContent()
		{
			yield return new XElement("title", Title.Value);

			yield return BaseLink.ToHtmlLink("base");

			foreach(var link in Links)
			{
				yield return link.ToHtmlLink("link");
			}
		}
	}
}