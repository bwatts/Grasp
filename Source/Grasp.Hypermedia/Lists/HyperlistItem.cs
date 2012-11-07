using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public class HyperlistItem : Notion
	{
		public static readonly Field<Hyperlink> LinkField = Field.On<HyperlistItem>.For(x => x.Link);
		public static readonly Field<ListItem> ListItemField = Field.On<HyperlistItem>.For(x => x.ListItem);

		public HyperlistItem(Hyperlink link, ListItem listItem)
		{
			Contract.Requires(link != null);
			Contract.Requires(listItem != null);

			Link = link;
			ListItem = listItem;
		}

		public Hyperlink Link { get { return GetValue(LinkField); } private set { SetValue(LinkField, value); } }
		public ListItem ListItem { get { return GetValue(ListItemField); } private set { SetValue(ListItemField, value); } }

		public object this[string field]
		{
			get { return ListItem[field]; }
		}
	}
}