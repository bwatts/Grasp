using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class HyperlistItems : Notion, IReadOnlyList<HyperlistItem>
	{
		public static readonly Field<ListSchema> SchemaField = Field.On<HyperlistItems>.For(x => x.Schema);
		public static readonly Field<ManyInOrder<HyperlistItem>> _itemsField = Field.On<HyperlistItems>.For(x => x._items);

		private ManyInOrder<HyperlistItem> _items { get { return GetValue(_itemsField); } set { SetValue(_itemsField, value); } }

		public HyperlistItems(ListSchema schema, IEnumerable<HyperlistItem> items)
		{
			Contract.Requires(schema != null);
			Contract.Requires(items != null);

			Schema = schema;
			_items = items.ToManyInOrder();
		}

		public ListSchema Schema { get { return GetValue(SchemaField); } private set { SetValue(SchemaField, value); } }

		public HyperlistItem this[int index]
		{
			get { return _items[index]; }
		}

		public int Count
		{
			get { return _items.Count; }
		}

		public IEnumerator<HyperlistItem> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}