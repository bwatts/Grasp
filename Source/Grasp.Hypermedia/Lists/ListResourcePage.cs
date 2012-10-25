using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Hypermedia.Lists
{
	public class ListResourcePage : Notion
	{
		public static readonly Field<Number> NumberField = Field.On<ListResourcePage>.For(x => x.Number);
		public static readonly Field<Count> SizeField = Field.On<ListResourcePage>.For(x => x.Size);
		public static readonly Field<Number> FirstItemField = Field.On<ListResourcePage>.For(x => x.FirstItem);
		public static readonly Field<Number> LastItemField = Field.On<ListResourcePage>.For(x => x.LastItem);
		public static readonly Field<ListItems> ItemsField = Field.On<ListResourcePage>.For(x => x.Items);

		public ListResourcePage(Number number, Count size, Number firstItem, Number lastItem, ListItems items)
		{
			Contract.Requires(firstItem <= lastItem);
			Contract.Requires(items != null);

			Number = number;
			Size = size;
			FirstItem = firstItem;
			LastItem = lastItem;
			Items = items;
		}

		public Number Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public Count Size { get { return GetValue(SizeField); } private set { SetValue(SizeField, value); } }
		public Number FirstItem { get { return GetValue(FirstItemField); } private set { SetValue(FirstItemField, value); } }
		public Number LastItem { get { return GetValue(LastItemField); } private set { SetValue(LastItemField, value); } }
		public ListItems Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }
	}
}