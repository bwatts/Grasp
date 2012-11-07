using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp;
using Grasp.Lists;
using Grasp.Raven;
using Grasp.Work;
using Raven.Client;
using Raven.Client.Linq;

namespace Grasp.Raven
{
	public abstract class RavenListService<T> : RavenContext, IListService where T : Notion
	{
		protected RavenListService(IDocumentStore documentStore) : base(documentStore)
		{}

		public Task<ListPage> GetPageAsync(ListPageKey key = null)
		{
			return ExecuteReadAsync(session =>
			{
				key = key ?? ListPageKey.Empty;

				var itemCount = Count.None;

				var items = Query(session).TakePage(key, out itemCount).ToList();

				return GetPage(key, itemCount, items);
			});
		}

		protected abstract IRavenQueryable<T> Query(IDocumentSession session);

		private ListPage GetPage(ListPageKey key, Count itemCount, List<T> items)
		{
			var pageCount = items.Count == 0 ? 0 : (itemCount.Value / key.Size.Value + 1);

			var resultKey = new ListPageKey(pageCount == 0 ? Number.None : key.Number, new Count(items.Count), key.Sort);

			var context = new ListPageContext(resultKey, new Count(pageCount), itemCount);

			return new ListPage(resultKey, context, new ListItems(GetSchema(), SelectListItems(context, items)));
		}

		private IEnumerable<ListItem> SelectListItems(ListPageContext context, IEnumerable<T> items)
		{
			var firstItem = context.PageKey.GetFirstItem();

			return items.Select((item, index) => new ListItem(firstItem + new Number(index), SelectBindings(context, item)));
		}

		protected abstract ListSchema GetSchema();

		protected abstract ListItemBindings SelectBindings(ListPageContext context, T item);
	}
}