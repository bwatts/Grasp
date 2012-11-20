using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Linq;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class Hyperlist : HttpResource
	{
		public static readonly Field<Hyperlink> PageLinkField = Field.On<Hyperlist>.For(x => x.PageLink);
		public static readonly Field<ListViewKey> QueryField = Field.On<Hyperlist>.For(x => x.Query);
		public static readonly Field<ListViewPages> PagesField = Field.On<Hyperlist>.For(x => x.Pages);
		public static readonly Field<HyperlistItems> ItemsField = Field.On<Hyperlist>.For(x => x.Items);

		public Hyperlist(MHeader header, Hyperlink pageLink, ListViewKey query, ListViewPages pages, HyperlistItems items) : base(header)
		{
			Contract.Requires(pageLink != null);
			Contract.Requires(query != null);
			Contract.Requires(pages != null);
			Contract.Requires(items != null);

			PageLink = pageLink;
			Query = query;
			Pages = pages;
			Items = items;
		}

		public Hyperlink PageLink { get { return GetValue(PageLinkField); } private set { SetValue(PageLinkField, value); } }
		public ListViewKey Query { get { return GetValue(QueryField); } private set { SetValue(QueryField, value); } }
		public ListViewPages Pages { get { return GetValue(PagesField); } private set { SetValue(PagesField, value); } }
		public HyperlistItems Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }
	}
}