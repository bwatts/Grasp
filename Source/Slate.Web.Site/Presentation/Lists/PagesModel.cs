using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Grasp;

namespace Slate.Web.Site.Presentation.Lists
{
	public class PagesModel : ViewModel
	{
		public static readonly Field<CountModel> CountField = Field.On<PagesModel>.For(x => x.Count);
		public static readonly Field<CountModel> CurrentField = Field.On<PagesModel>.For(x => x.Current);
		public static readonly Field<CountModel> FirstField = Field.On<PagesModel>.For(x => x.First);
		public static readonly Field<CountModel> LastField = Field.On<PagesModel>.For(x => x.Last);
		public static readonly Field<CountModel> PreviousField = Field.On<PagesModel>.For(x => x.Previous);
		public static readonly Field<CountModel> NextField = Field.On<PagesModel>.For(x => x.Next);
		public static readonly Field<ManyInOrder<CountModel>> AllField = Field.On<PagesModel>.For(x => x.All);

		public PagesModel(
			CountModel count,
			CountModel current = null,
			CountModel first = null,
			CountModel last = null,
			CountModel previous = null,
			CountModel next = null,
			IEnumerable<CountModel> all = null)
		{
			Contract.Requires(count != null);
			Contract.Requires(all != null);

			Count = count;
			Current = current;
			First = first;
			Last = last;
			Previous = previous;
			Next = next;
			All = (all ?? Enumerable.Empty<CountModel>()).ToManyInOrder();
		}

		public CountModel Count { get { return GetValue(CountField); } private set { SetValue(CountField, value); } }
		public CountModel Current { get { return GetValue(CurrentField); } private set { SetValue(CurrentField, value); } }
		public CountModel First { get { return GetValue(FirstField); } private set { SetValue(FirstField, value); } }
		public CountModel Last { get { return GetValue(LastField); } private set { SetValue(LastField, value); } }
		public CountModel Previous { get { return GetValue(PreviousField); } private set { SetValue(PreviousField, value); } }
		public CountModel Next { get { return GetValue(NextField); } private set { SetValue(NextField, value); } }
		public ManyInOrder<CountModel> All { get { return GetValue(AllField); } private set { SetValue(AllField, value); } }
	}
}