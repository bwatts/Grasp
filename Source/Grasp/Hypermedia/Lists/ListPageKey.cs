using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp.Hypermedia.Lists
{
	/// <summary>
	/// The unique identifier of a list page, composed of the page number, size, and sort fields
	/// </summary>
	public sealed class ListPageKey : EquatableNotion<ListPageKey>
	{
		public static readonly Field<Number> NumberField = Field.On<ListPageKey>.For(x => x.Number);
		public static readonly Field<Count> SizeField = Field.On<ListPageKey>.For(x => x.Size);
		public static readonly Field<Sort> SortField = Field.On<ListPageKey>.For(x => x.Sort);

		public static readonly ListPageKey Empty = new ListPageKey();

		public ListPageKey(Number number = default(Number), Count size = default(Count), Sort sort = null)
		{
			Number = number;
			Size = size;
			Sort = sort ?? Sort.Empty;
		}

		public Number Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public Count Size { get { return GetValue(SizeField); } private set { SetValue(SizeField, value); } }
		public Sort Sort { get { return GetValue(SortField); } private set { SetValue(SortField, value); } }

		public override bool Equals(ListPageKey other)
		{
			return other != null && Number == other.Number && Size == other.Size && Sort == other.Sort;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Number, Size, Sort);
		}

		public Number GetFirstItem()
		{
			return new Number((Number.Value - 1) * Size.Value + 1);
		}

		public Number GetLastItem(Count itemCount)
		{
			return new Number(GetFirstItem().Value + itemCount.Value);
		}

		public HrefQuery GetQuery(string pageKey = "page", string pageSizeKey = "size", string sortKey = "sort")
		{
			var query = new StringBuilder();

			if(Number != Number.None)
			{
				query.Append(pageKey).Append("=").Append(Number);

				if(Size != Count.None)
				{
					query.Append("&").Append(pageSizeKey).Append("=").Append(Size);
				}

				if(Sort != Sort.Empty)
				{
					query.Append("&").Append(sortKey).Append("=").Append(Sort);
				}
			}

			return new HrefQuery(query.ToString());
		}
	}
}