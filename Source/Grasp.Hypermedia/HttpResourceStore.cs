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
		protected static Hyperlist GetList(
			MHeader header,
			Hyperlink pageLink,
			ListPageKey query,
			ListPage page,
			Func<ListItem, HyperlistItem> hyperlistItemSelector)
		{
			Contract.Requires(header != null);
			Contract.Requires(pageLink != null);
			Contract.Requires(query != null);
			Contract.Requires(page != null);
			Contract.Requires(hyperlistItemSelector != null);

			return new Hyperlist(header, pageLink, query, page.Context, CreatePage(page, hyperlistItemSelector));
		}

		private static HyperlistPage CreatePage(ListPage page, Func<ListItem, HyperlistItem> hyperlistItemSelector)
		{
			return new HyperlistPage(
				page.Key.Number,
				page.Key.Size,
				page.FirstItemNumber,
				page.LastItemNumber,
				new HyperlistItems(page.Items.Schema, page.Items.Select(hyperlistItemSelector)));
		}
	}
}