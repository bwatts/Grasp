using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Lists
{
	public class ListPage : Notion
	{
		public static readonly Field<ListPageKey> KeyField = Field.On<ListPage>.For(x => x.Key);
		public static readonly Field<ListPageContext> ContextField = Field.On<ListPage>.For(x => x.Context);
		public static readonly Field<ListItems> ItemsField = Field.On<ListPage>.For(x => x.Items);

		public ListPage(ListPageKey key, ListPageContext context, ListItems items)
		{
			Contract.Requires(key != null);
			Contract.Requires(context != null);
			Contract.Requires(items != null);
			Contract.Requires(items.Count <= key.Size.Value);

			Key = key;
			Context = context;
			Items = items;
		}

		public ListPageKey Key { get { return GetValue(KeyField); } private set { SetValue(KeyField, value); } }
		public new ListPageContext Context { get { return GetValue(ContextField); } private set { SetValue(ContextField, value); } }
		public ListItems Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		public bool IsEmpty
		{
			get { return Key.Size == Count.None; }
		}

		public Number FirstItem
		{
			get { return Context.ItemCount == Count.None ? Number.None : Key.GetFirstItem(); }
		}

		public Number LastItem
		{
			get { return Context.ItemCount == Count.None ? Number.None : Key.GetLastItem(Context.ItemCount); }
		}
	}
}