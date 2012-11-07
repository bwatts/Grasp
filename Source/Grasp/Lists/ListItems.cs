using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Lists
{
	public class ListItems : Notion, IReadOnlyList<ListItem>
	{
		public static readonly Field<ListSchema> SchemaField = Field.On<ListItems>.For(x => x.Schema);
		public static readonly Field<ManyInOrder<ListItem>> _itemsField = Field.On<ListItems>.For(x => x._items);

		private ManyInOrder<ListItem> _items { get { return GetValue(_itemsField); } set { SetValue(_itemsField, value); } }

		public ListItems(ListSchema schema, IEnumerable<ListItem> items)
		{
			Contract.Requires(schema != null);
			Contract.Requires(items != null);

			Schema = schema;
			_items = new ManyInOrder<ListItem>(items);
		}

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