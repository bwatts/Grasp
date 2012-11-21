using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;

namespace Grasp.Lists
{
	/// <summary>
	/// The unique identifier of a list view, composed of the start item, size, and sort fields
	/// </summary>
	public sealed class ListViewKey : EquatableNotion<ListViewKey>
	{
		public static readonly Field<Count> StartField = Field.On<ListViewKey>.For(x => x.Start);
		public static readonly Field<Count> SizeField = Field.On<ListViewKey>.For(x => x.Size);
		public static readonly Field<Sort> SortField = Field.On<ListViewKey>.For(x => x.Sort);

		public static readonly ListViewKey Empty = new ListViewKey();
		public static readonly ListViewKey Default = new ListViewKey(start: new Count(1), size: new Count(10));

		public ListViewKey(Count start = default(Count), Count size = default(Count), Sort sort = null)
		{
			Start = start;
			Size = size;
			Sort = sort ?? Sort.Empty;
		}

		public Count Start { get { return GetValue(StartField); } private set { SetValue(StartField, value); } }
		public Count Size { get { return GetValue(SizeField); } private set { SetValue(SizeField, value); } }
		public Sort Sort { get { return GetValue(SortField); } private set { SetValue(SortField, value); } }

		public override bool Equals(ListViewKey other)
		{
			return other != null && Start == other.Start && Size == other.Size && Sort == other.Sort;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Start, Size, Sort);
		}

		public Count End
		{
			get { return Start == Count.None ? Count.None : new Count(Start.Value + Size.Value - 1); }
		}

		public Count GetPageStart(Count number)
		{
			return new Count((number.Value - 1) * Size.Value + 1);
		}

		public string GetQuery(string startParameter = "start", string sizeParameter = "size", string sortParameter = "sort", bool includeSeparator = false)
		{
			Contract.Requires(!String.IsNullOrEmpty(startParameter));
			Contract.Requires(!String.IsNullOrEmpty(sizeParameter));
			Contract.Requires(!String.IsNullOrEmpty(sortParameter));

			var query = new StringBuilder();

			if(Start != Count.None)
			{
				if(includeSeparator)
				{
					query.Append("?");
				}

				query.Append(startParameter).Append("=").Append(Start);

				if(Size != Count.None)
				{
					query.Append("&").Append(sizeParameter).Append("=").Append(Size);
				}

				if(Sort != Sort.Empty)
				{
					query.Append("&").Append(sortParameter).Append("=").Append(Sort);
				}
			}

			return query.ToString();
		}
	}
}