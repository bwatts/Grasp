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
	public class HyperlistQuery : Notion
	{
		public static readonly Field<Hyperlink> ListLinkField = Field.On<HyperlistQuery>.For(x => x.ListLink);
		public static readonly Field<ListViewKey> KeyField = Field.On<HyperlistQuery>.For(x => x.Key);
		public static readonly Field<string> StartParameterField = Field.On<HyperlistQuery>.For(x => x.StartParameter);
		public static readonly Field<string> SizeParameterField = Field.On<HyperlistQuery>.For(x => x.SizeParameter);
		public static readonly Field<string> SortParameterField = Field.On<HyperlistQuery>.For(x => x.SortParameter);

		public HyperlistQuery(Hyperlink listLink, ListViewKey key, string startParameter, string sizeParameter, string sortParameter)
		{
			Contract.Requires(listLink != null);
			Contract.Requires(key != null);
			Contract.Requires(startParameter != null);
			Contract.Requires(sizeParameter != null);
			Contract.Requires(sortParameter != null);

			ListLink = listLink;
			Key = key;
			StartParameter = startParameter;
			SizeParameter = sizeParameter;
			SortParameter = sortParameter;
		}

		public Hyperlink ListLink { get { return GetValue(ListLinkField); } private set { SetValue(ListLinkField, value); } }
		public ListViewKey Key { get { return GetValue(KeyField); } private set { SetValue(KeyField, value); } }
		public string StartParameter { get { return GetValue(StartParameterField); } private set { SetValue(StartParameterField, value); } }
		public string SizeParameter { get { return GetValue(SizeParameterField); } private set { SetValue(SizeParameterField, value); } }
		public string SortParameter { get { return GetValue(SortParameterField); } private set { SetValue(SortParameterField, value); } }

		public string GetQueryString(bool includeSeparator = false)
		{
			return Key.GetQuery(StartParameter, SizeParameter, SortParameter, includeSeparator);
		}
	}
}