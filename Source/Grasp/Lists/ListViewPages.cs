using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Lists
{
	public class ListViewPages : Notion
	{
		public static readonly Field<Count> CountField = Field.On<ListViewPages>.For(x => x.Count);
		public static readonly Field<Count> CurrentField = Field.On<ListViewPages>.For(x => x.Current);
		public static readonly Field<Count> PreviousField = Field.On<ListViewPages>.For(x => x.Previous);
		public static readonly Field<Count> NextField = Field.On<ListViewPages>.For(x => x.Next);

		public ListViewPages(ListViewKey key, Count totalItemCount)
		{
			Contract.Requires(key != null);

			if(key.Size == Count.None)
			{
				Count = Count.None;
				Current = Count.None;
				Previous = Count.None;
				Next = Count.None;
			}
			else
			{
				Count = new Count((totalItemCount.Value - 1) / key.Size.Value + 1);
				Current = new Count((key.Start.Value - 1) / key.Size.Value + 1);
				Previous = Current.Value == 1 ? Count.None : Current - 1;
				Next = Current.Value == Count.Value ? Count.None : Current + 1;
			}
		}

		public Count Count { get { return GetValue(CountField); } private set { SetValue(CountField, value); } }
		public Count Current { get { return GetValue(CurrentField); } private set { SetValue(CurrentField, value); } }
		public Count Previous { get { return GetValue(PreviousField); } private set { SetValue(PreviousField, value); } }
		public Count Next { get { return GetValue(NextField); } private set { SetValue(NextField, value); } }
	}
}