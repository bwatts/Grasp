using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Lists
{
	public class ListPageContext : Notion
	{
		public static readonly Field<ListPageKey> PageKeyField = Field.On<ListPageContext>.For(x => x.PageKey);
		public static readonly Field<Count> PageCountField = Field.On<ListPageContext>.For(x => x.PageCount);
		public static readonly Field<Count> ItemCountField = Field.On<ListPageContext>.For(x => x.ItemCount);

		public ListPageContext(ListPageKey pageKey, Count pageCount, Count itemCount)
		{
			Contract.Requires(pageKey != null);

			PageKey = pageKey;
			PageCount = pageCount;
			ItemCount = itemCount;
		}

		public ListPageKey PageKey { get { return GetValue(PageKeyField); } private set { SetValue(PageKeyField, value); } }
		public Count PageCount { get { return GetValue(PageCountField); } private set { SetValue(PageCountField, value); } }
		public Count ItemCount { get { return GetValue(ItemCountField); } private set { SetValue(ItemCountField, value); } }

		public Number PreviousPage
		{
			get { return PageKey.Number == Number.None || PageKey.Number == Number.First ? PageKey.Number : PageKey.Number - 1; }
		}

		public Number NextPage
		{
			get { return PageKey.Number.Value == PageCount.Value ? PageKey.Number : PageKey.Number + 1; }
		}
	}
}