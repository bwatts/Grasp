using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class Hyperlist : HttpResource
	{
		public static readonly Field<Hyperlink> PageLinkField = Field.On<Hyperlist>.For(x => x.PageLink);
		public static readonly Field<ListPageKey> QueryField = Field.On<Hyperlist>.For(x => x.Query);
		public static readonly Field<ListPageContext> ContextField = Field.On<Hyperlist>.For(x => x.Context);
		public static readonly Field<HyperlistPage> PageField = Field.On<Hyperlist>.For(x => x.Page);

		public Hyperlist(HttpResourceHeader header, Hyperlink pageLink, ListPageKey query, ListPageContext context, HyperlistPage page) : base(header)
		{
			Contract.Requires(pageLink != null);
			Contract.Requires(query != null);
			Contract.Requires(context != null);
			Contract.Requires(page != null);

			PageLink = pageLink;
			Query = query;
			Context = context;
			Page = page;
		}

		public Hyperlink PageLink { get { return GetValue(PageLinkField); } private set { SetValue(PageLinkField, value); } }
		public ListPageKey Query { get { return GetValue(QueryField); } private set { SetValue(QueryField, value); } }
		public ListPageContext Context { get { return GetValue(ContextField); } private set { SetValue(ContextField, value); } }
		public HyperlistPage Page { get { return GetValue(PageField); } private set { SetValue(PageField, value); } }
	}
}