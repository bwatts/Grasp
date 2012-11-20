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
	public abstract class RavenListService<TItem> : RavenContext, IListService where TItem : Notion
	{
		protected RavenListService(IDocumentStore documentStore) : base(documentStore)
		{}

		public Task<ListView> GetViewAsync(ListViewKey key = null)
		{
			return ExecuteReadAsync(session =>
			{
				key = key ?? ListViewKey.Default;

				var totalItems = Count.None;

				var items = Query(session).TakePage(key, out totalItems).ToList();

				return GetView(key, totalItems, items);
			});
		}

		protected abstract IRavenQueryable<TItem> Query(IDocumentSession session);

		private ListView GetView(ListViewKey key, Count totalItems, List<TItem> items)
		{
			var pageCount = items.Count == 0 ? 0 : (totalItems.Value / key.Size.Value + 1);

			var resultKey = new ListViewKey(pageCount == 0 ? Count.None : key.Start, new Count(items.Count), key.Sort);

			return new ListView(resultKey, new ListViewItems(totalItems, GetSchema(), SelectListItems(resultKey, items)));
		}

		private IEnumerable<ListItem> SelectListItems(ListViewKey key, IEnumerable<TItem> items)
		{
			var start = key.Start;

			return items.Select((item, index) => new ListItem(start + new Count(index), SelectBindings(item)));
		}

		protected abstract ListSchema GetSchema();

		protected abstract ListItemBindings SelectBindings(TItem item);
	}
}