using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Web.Site.Presentation.Lists
{
	public class PageContextModel : ViewModel
	{
		public static readonly Field<NumberModel> ItemCountField = Field.On<PageContextModel>.For(x => x.ItemCount);
		public static readonly Field<NumberModel> FirstField = Field.On<PageContextModel>.For(x => x.First);
		public static readonly Field<NumberModel> LastField = Field.On<PageContextModel>.For(x => x.Last);
		public static readonly Field<NumberModel> PreviousField = Field.On<PageContextModel>.For(x => x.Previous);
		public static readonly Field<NumberModel> NextField = Field.On<PageContextModel>.For(x => x.Next);
		public static readonly Field<ManyInOrder<NumberModel>> PagesField = Field.On<PageContextModel>.For(x => x.Pages);

		public PageContextModel(Hyperlink itemCountLink)
		{
			Contract.Requires(itemCountLink != null);

			ItemCount = new NumberModel(Number.None, itemCountLink);

			Pages = new ManyInOrder<NumberModel>();
		}

		public PageContextModel(NumberModel itemCount, NumberModel first, NumberModel last, NumberModel previous, NumberModel next, IEnumerable<NumberModel> pages)
		{
			Contract.Requires(itemCount != null);
			Contract.Requires(first != null);
			Contract.Requires(last != null);
			Contract.Requires(previous != null);
			Contract.Requires(next != null);
			Contract.Requires(pages != null);

			ItemCount = itemCount;
			First = first;
			Last = last;
			Previous = previous;
			Next = next;
			Pages = pages.ToManyInOrder();
		}

		public NumberModel ItemCount { get { return GetValue(ItemCountField); } private set { SetValue(ItemCountField, value); } }
		public NumberModel First { get { return GetValue(FirstField); } private set { SetValue(FirstField, value); } }
		public NumberModel Last { get { return GetValue(LastField); } private set { SetValue(LastField, value); } }
		public NumberModel Previous { get { return GetValue(PreviousField); } private set { SetValue(PreviousField, value); } }
		public NumberModel Next { get { return GetValue(NextField); } private set { SetValue(NextField, value); } }
		public ManyInOrder<NumberModel> Pages { get { return GetValue(PagesField); } private set { SetValue(PagesField, value); } }
	}
}