using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Grasp.Hypermedia.Linq
{
	public sealed class MHeader : Notion
	{
		public static readonly Field<string> TitleField = Field.On<MHeader>.For(x => x.Title);
		public static readonly Field<Hyperlink> BaseLinkField = Field.On<MHeader>.For(x => x.BaseLink);
		public static readonly Field<Hyperlink> SelfLinkField = Field.On<MHeader>.For(x => x.SelfLink);
		public static readonly Field<Many<Hyperlink>> LinksField = Field.On<MHeader>.For(x => x.Links);

		public MHeader(string title, Hyperlink baseLink, Hyperlink selfLink, IEnumerable<Hyperlink> links)
		{
			Contract.Requires(title != null);
			Contract.Requires(baseLink != null);
			Contract.Requires(selfLink != null);
			Contract.Requires(selfLink.Relationship == Relationship.Self);
			Contract.Requires(links != null);

			Title = title;
			BaseLink = baseLink;
			SelfLink = selfLink;
			Links = links.ToMany();
		}

		public MHeader(string title, Hyperlink baseLink, Hyperlink selfLink, params Hyperlink[] links) : this(title, baseLink, selfLink, links as IEnumerable<Hyperlink>)
		{}

		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public Hyperlink BaseLink { get { return GetValue(BaseLinkField); } private set { SetValue(BaseLinkField, value); } }
		public Hyperlink SelfLink { get { return GetValue(SelfLinkField); } private set { SetValue(SelfLinkField, value); } }
		public Many<Hyperlink> Links { get { return GetValue(LinksField); } private set { SetValue(LinksField, value); } }

		internal object GetHtml()
		{
			return new XElement("head", GetHeadContent());
		}

		private IEnumerable<XElement> GetHeadContent()
		{
			yield return new XElement("title", Title);

			if(BaseLink != null)
			{
				yield return BaseLink.ToHtml("base");
			}

			if(SelfLink != null)
			{
				yield return SelfLink.ToHtml("link");
			}

			foreach(var link in Links)
			{
				yield return link.ToHtml("link");
			}
		}
	}
}