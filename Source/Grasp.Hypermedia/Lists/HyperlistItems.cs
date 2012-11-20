using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class HyperlistItems : Notion, IReadOnlyList<HyperlistItem>
	{
		public static readonly Field<Count> TotalField = Field.On<HyperlistItems>.For(x => x.Total);
		public static readonly Field<ListSchema> SchemaField = Field.On<HyperlistItems>.For(x => x.Schema);
		public static readonly Field<ManyInOrder<HyperlistItem>> _itemsField = Field.On<HyperlistItems>.For(x => x._items);

		private ManyInOrder<HyperlistItem> _items { get { return GetValue(_itemsField); } set { SetValue(_itemsField, value); } }

		public HyperlistItems(Count total, ListSchema schema, IEnumerable<HyperlistItem> items)
		{
			Contract.Requires(schema != null);
			Contract.Requires(items != null);

			Total = total;
			Schema = schema;
			_items = items.ToManyInOrder();
		}

		public Count Total { get { return GetValue(TotalField); } private set { SetValue(TotalField, value); } }
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