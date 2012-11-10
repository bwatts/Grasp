using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia
{
	public class HttpResourceHeader : Notion
	{
		public static readonly Field<string> TitleField = Field.On<HttpResourceHeader>.For(x => x.Title);
		public static readonly Field<Hyperlink> BaseLinkField = Field.On<HttpResourceHeader>.For(x => x.BaseLink);
		public static readonly Field<ManyInOrder<Hyperlink>> LinksField = Field.On<HttpResourceHeader>.For(x => x.Links);

		public HttpResourceHeader(string title, Hyperlink baseLink, IEnumerable<Hyperlink> links)
		{
			Contract.Requires(title != null);
			Contract.Requires(baseLink != null);
			Contract.Requires(links != null);

			Title = title;
			BaseLink = baseLink;
			Links = links.ToManyInOrder();
		}

		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public Hyperlink BaseLink { get { return GetValue(BaseLinkField); } private set { SetValue(BaseLinkField, value); } }
		public ManyInOrder<Hyperlink> Links { get { return GetValue(LinksField); } private set { SetValue(LinksField, value); } }
	}
}