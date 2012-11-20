using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Lists
{
	public class ListView : Notion
	{
		public static readonly Field<ListViewKey> KeyField = Field.On<ListView>.For(x => x.Key);
		public static readonly Field<ListViewPages> PagesField = Field.On<ListView>.For(x => x.Pages);
		public static readonly Field<ListViewItems> ItemsField = Field.On<ListView>.For(x => x.Items);

		public ListView(ListViewKey key, ListViewItems items)
		{
			Contract.Requires(key != null);
			Contract.Requires(items != null);
			Contract.Requires(items.Count <= key.Size.Value);

			Key = key;
			Items = items;

			Pages = new ListViewPages(key, Items.Total);
		}

		public ListViewKey Key { get { return GetValue(KeyField); } private set { SetValue(KeyField, value); } }
		public ListViewPages Pages { get { return GetValue(PagesField); } private set { SetValue(PagesField, value); } }
		public ListViewItems Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		public bool IsEmpty
		{
			get { return Key.Size == Count.None; }
		}

		public ListItem FirstItem
		{
			get { return IsEmpty ? null : Items[Key.Start.Value]; }
		}

		public ListItem LastItem
		{
			get { return IsEmpty ? null : Items[Key.End.Value]; }
		}
	}
}