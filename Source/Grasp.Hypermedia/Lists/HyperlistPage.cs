using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class HyperlistPage : Notion
	{
		public static readonly Field<Number> NumberField = Field.On<HyperlistPage>.For(x => x.Number);
		public static readonly Field<Count> SizeField = Field.On<HyperlistPage>.For(x => x.Size);
		public static readonly Field<Number> FirstItemField = Field.On<HyperlistPage>.For(x => x.FirstItem);
		public static readonly Field<Number> LastItemField = Field.On<HyperlistPage>.For(x => x.LastItem);
		public static readonly Field<HyperlistItems> ItemsField = Field.On<HyperlistPage>.For(x => x.Items);

		public HyperlistPage(Number number, Count size, Number firstItem, Number lastItem, HyperlistItems items)
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
		public HyperlistItems Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }
	}
}