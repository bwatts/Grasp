using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Linq;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Grasp.Hypermedia
{
	public abstract class HttpResourceStore : Notion
	{
		protected static Hyperlist GetHyperlist(MHeader header, Hyperlink pageLink, ListViewKey query, ListView list, Func<ListItem, HyperlistItem> itemSelector)
		{
			Contract.Requires(header != null);
			Contract.Requires(pageLink != null);
			Contract.Requires(query != null);
			Contract.Requires(list != null);
			Contract.Requires(itemSelector != null);

			return new Hyperlist(header, pageLink, query, list.Pages, CreateItems(list, itemSelector));
		}

		private static HyperlistItems CreateItems(ListView list, Func<ListItem, HyperlistItem> itemSelector)
		{
			return new HyperlistItems(list.Items.Total, list.Items.Schema, list.Items.Select(itemSelector));
		}
	}
}