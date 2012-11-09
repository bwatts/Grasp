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
		public static readonly Field<Number> FirstItemNumberField = Field.On<HyperlistPage>.For(x => x.FirstItemNumber);
		public static readonly Field<Number> LastItemNumberField = Field.On<HyperlistPage>.For(x => x.LastItemNumber);
		public static readonly Field<HyperlistItems> ItemsField = Field.On<HyperlistPage>.For(x => x.Items);

		public HyperlistPage(Number number, Count size, Number firstItemNumber, Number lastItemNumber, HyperlistItems items)
		{
			Contract.Requires(firstItemNumber <= lastItemNumber);
			Contract.Requires(items != null);

			Number = number;
			Size = size;
			FirstItemNumber = firstItemNumber;
			LastItemNumber = lastItemNumber;
			Items = items;
		}

		public Number Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public Count Size { get { return GetValue(SizeField); } private set { SetValue(SizeField, value); } }
		public Number FirstItemNumber { get { return GetValue(FirstItemNumberField); } private set { SetValue(FirstItemNumberField, value); } }
		public Number LastItemNumber { get { return GetValue(LastItemNumberField); } private set { SetValue(LastItemNumberField, value); } }
		public HyperlistItems Items { get { return GetValue(ItemsField); } private set { SetValue(ItemsField, value); } }

		public HyperlistItem GetFirstItem()
		{
			return FirstItemNumber == Number.None ? null : Items[FirstItemNumber.Value - 1];
		}

		public HyperlistItem GetLastItem()
		{
			return LastItemNumber == Number.None ? null : Items[LastItemNumber.Value - 1];
		}
	}
}