using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Lists
{
	public class ListViewItems : Notion, IReadOnlyList<ListItem>
	{
		public static readonly Field<Count> TotalField = Field.On<ListViewItems>.For(x => x.Total);
		public static readonly Field<ListSchema> SchemaField = Field.On<ListViewItems>.For(x => x.Schema);
		public static readonly Field<ManyInOrder<ListItem>> _itemsField = Field.On<ListViewItems>.For(x => x._items);

		private ManyInOrder<ListItem> _items { get { return GetValue(_itemsField); } set { SetValue(_itemsField, value); } }

		public ListViewItems(Count total, ListSchema schema, IEnumerable<ListItem> items)
		{
			Contract.Requires(schema != null);
			Contract.Requires(items != null);

			Total = total;
			Schema = schema;
			_items = items.ToManyInOrder();
		}

		public Count Total { get { return GetValue(TotalField); } private set { SetValue(TotalField, value); } }
		public ListSchema Schema { get { return GetValue(SchemaField); } private set { SetValue(SchemaField, value); } }

		public ListItem this[int index]
		{
			get { return _items[index]; }
		}

		public int Count
		{
			get { return _items.Count; }
		}

		public IEnumerator<ListItem> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}