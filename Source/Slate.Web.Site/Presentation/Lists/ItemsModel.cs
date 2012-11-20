using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Grasp;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	public class ItemsModel : ViewModel, IReadOnlyList<ItemModel>
	{
		public static readonly Field<CountModel> TotalField = Field.On<ItemsModel>.For(x => x.Total);
		public static readonly Field<CountModel> FirstField = Field.On<ItemsModel>.For(x => x.First);
		public static readonly Field<CountModel> LastField = Field.On<ItemsModel>.For(x => x.Last);
		public static readonly Field<ListSchema> SchemaField = Field.On<ItemsModel>.For(x => x.Schema);
		public static readonly Field<ManyInOrder<ItemModel>> _itemsField = Field.On<ItemsModel>.For(x => x._items);

		private ManyInOrder<ItemModel> _items { get { return GetValue(_itemsField); } set { SetValue(_itemsField, value); } }

		public ItemsModel(CountModel total, CountModel first, CountModel last, ListSchema schema, IEnumerable<ItemModel> items)
		{
			Contract.Requires(total != null);
			Contract.Requires(first != null);
			Contract.Requires(last != null);
			Contract.Requires(schema != null);
			Contract.Requires(items != null);

			Total = total;
			First = first;
			Last = last;
			Schema = schema;
			_items = items.ToManyInOrder();
		}

		public CountModel Total { get { return GetValue(TotalField); } private set { SetValue(TotalField, value); } }
		public CountModel First { get { return GetValue(FirstField); } private set { SetValue(FirstField, value); } }
		public CountModel Last { get { return GetValue(LastField); } private set { SetValue(LastField, value); } }
		public ListSchema Schema { get { return GetValue(SchemaField); } private set { SetValue(SchemaField, value); } }

		public ItemModel this[int index]
		{
			get { return _items[index]; }
		}

		public int Count
		{
			get { return _items.Count; }
		}

		public IEnumerator<ItemModel> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}