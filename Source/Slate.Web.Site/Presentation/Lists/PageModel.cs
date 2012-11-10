using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	public class PageModel : ViewModel
	{
		public static readonly Field<NumberModel> NumberField = Field.On<PageModel>.For(x => x.Number);
		public static readonly Field<Count> SizeField = Field.On<PageModel>.For(x => x.Size);
		public static readonly Field<NumberModel> FirstItemField = Field.On<PageModel>.For(x => x.FirstItem);
		public static readonly Field<NumberModel> LastItemField = Field.On<PageModel>.For(x => x.LastItem);
		public static readonly Field<ListSchema> SchemaField = Field.On<PageModel>.For(x => x.Schema);
		public static readonly Field<ManyInOrder<ItemModel>> ItemsField = Field.On<PageModel>.For(x => x.Items);

		public PageModel(NumberModel number, Count size, NumberModel firstItem, NumberModel lastItem, ListSchema schema, IEnumerable<ItemModel> items)
		{
			Contract.Requires(number != null);
			Contract.Requires(firstItem != null);
			Contract.Requires(lastItem != null);
			Contract.Requires(firstItem.Value <= lastItem.Value);
			Contract.Requires(schema != null);
			Contract.Requires(items != null);

			Number = number;
			Size = size;
			FirstItem = firstItem;
			LastItem = lastItem;
			Schema = schema;
			Items = items.ToManyInOrder();
		}

		public NumberModel Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public Count Size { get { return GetValue(SizeField); } private set { SetValue(SizeField, value); } }
		public NumberModel FirstItem { get { return GetValue(FirstItemField); } private set { SetValue(FirstItemField, value); } }
		public NumberModel LastItem { get { return GetValue(LastItemField); } private set { SetValue(LastItemField, value); } }
		public ListSchema Schema { get { return GetValue(SchemaField); } private set { SetValue(SchemaField, value); } }
		public ManyInOrder<ItemModel> Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }
	}
}