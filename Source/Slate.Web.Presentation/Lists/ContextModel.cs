using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;

namespace Slate.Web.Presentation.Lists
{
	public class ContextModel : ViewModel
	{
		public static readonly Field<NumberModel> ItemCountField = Field.On<ContextModel>.For(x => x.ItemCount);
		public static readonly Field<NumberModel> FirstField = Field.On<ContextModel>.For(x => x.First);
		public static readonly Field<NumberModel> LastField = Field.On<ContextModel>.For(x => x.Last);
		public static readonly Field<NumberModel> PreviousField = Field.On<ContextModel>.For(x => x.Previous);
		public static readonly Field<NumberModel> NextField = Field.On<ContextModel>.For(x => x.Next);
		public static readonly Field<ManyInOrder<NumberModel>> NumbersField = Field.On<ContextModel>.For(x => x.Numbers);

		public ContextModel(HtmlLink itemCountLink)
		{
			Contract.Requires(itemCountLink != null);

			ItemCount = new NumberModel(Number.None, itemCountLink);

			Numbers = new ManyInOrder<NumberModel>();
		}

		public ContextModel(NumberModel itemCount, NumberModel first, NumberModel last, NumberModel previous, NumberModel next, IEnumerable<NumberModel> numbers)
		{
			Contract.Requires(itemCount != null);
			Contract.Requires(first != null);
			Contract.Requires(last != null);
			Contract.Requires(previous != null);
			Contract.Requires(next != null);
			Contract.Requires(numbers != null);

			ItemCount = itemCount;
			First = first;
			Last = last;
			Previous = previous;
			Next = next;
			Numbers = new ManyInOrder<NumberModel>(numbers);
		}

		public NumberModel ItemCount { get { return GetValue(ItemCountField); } private set { SetValue(ItemCountField, value); } }
		public NumberModel First { get { return GetValue(FirstField); } private set { SetValue(FirstField, value); } }
		public NumberModel Last { get { return GetValue(LastField); } private set { SetValue(LastField, value); } }
		public NumberModel Previous { get { return GetValue(PreviousField); } private set { SetValue(PreviousField, value); } }
		public NumberModel Next { get { return GetValue(NextField); } private set { SetValue(NextField, value); } }
		public ManyInOrder<NumberModel> Numbers { get { return GetValue(NumbersField); } private set { SetValue(NumbersField, value); } }
	}
}