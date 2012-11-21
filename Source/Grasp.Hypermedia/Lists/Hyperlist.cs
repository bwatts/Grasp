using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Hypermedia.Linq;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class Hyperlist : HttpResource
	{
		public static readonly Field<HyperlistQuery> QueryField = Field.On<Hyperlist>.For(x => x.Query);
		public static readonly Field<ListViewPages> PagesField = Field.On<Hyperlist>.For(x => x.Pages);
		public static readonly Field<HyperlistItems> ItemsField = Field.On<Hyperlist>.For(x => x.Items);

		public Hyperlist(MHeader header, HyperlistQuery query, ListViewPages pages, HyperlistItems items) : base(header)
		{
			Contract.Requires(query != null);
			Contract.Requires(pages != null);
			Contract.Requires(items != null);

			Query = query;
			Pages = pages;
			Items = items;
		}

		public HyperlistQuery Query { get { return GetValue(QueryField); } private set { SetValue(QueryField, value); } }
		public ListViewPages Pages { get { return GetValue(PagesField); } private set { SetValue(PagesField, value); } }
		public HyperlistItems Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }
	}
}