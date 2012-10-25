using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Hypermedia.Lists
{
	public class ListResource : HttpResource
	{
		public static readonly Field<ListPageKey> QueryField = Field.On<ListResource>.For(x => x.Query);
		public static readonly Field<ListPageContext> ContextField = Field.On<ListResource>.For(x => x.Context);
		public static readonly Field<ListResourcePage> PageField = Field.On<ListResource>.For(x => x.Page);

		public ListResource(HttpResourceHeader header, ListPageKey query, ListPageContext context, ListResourcePage page) : base(header)
		{
			Contract.Requires(query != null);
			Contract.Requires(context != null);
			Contract.Requires(page != null);

			Query = query;
			Context = context;
			Page = page;
		}

		public ListPageKey Query { get { return GetValue(QueryField); } private set { SetValue(QueryField, value); } }
		public ListPageContext Context { get { return GetValue(ContextField); } private set { SetValue(ContextField, value); } }
		public ListResourcePage Page { get { return GetValue(PageField); } private set { SetValue(PageField, value); } }
	}
}