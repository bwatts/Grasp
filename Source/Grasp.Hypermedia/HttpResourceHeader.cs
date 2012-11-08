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
		public static readonly Field<ManyInOrder<Hyperlink>> LinksField = Field.On<HttpResourceHeader>.For(x => x.Links);

		public HttpResourceHeader(string title, Hyperlink baseLink, IEnumerable<Hyperlink> links)
		{
			Contract.Requires(title != null);
			Contract.Requires(links != null);

			Title = title;
			Links = new ManyInOrder<Hyperlink>(links);
		}

		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
		public ManyInOrder<Hyperlink> Links { get { return GetValue(LinksField); } private set { SetValue(LinksField, value); } }
	}
}