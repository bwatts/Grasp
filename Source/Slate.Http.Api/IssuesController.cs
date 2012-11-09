using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Cloak;
using Cloak.Http.Media;
using Cloak.Reflection;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Linq;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Http.Api
{
	public class IssuesController : ApiController
	{
		private readonly IHttpResourceContext _resourceContext;
		private readonly IListService _listService;

		public IssuesController(IHttpResourceContext resourceContext, IListService listService)
		{
			Contract.Requires(resourceContext != null);
			Contract.Requires(listService != null);

			_resourceContext = resourceContext;
			_listService = listService;
		}

		public async Task<Hyperlist> GetListPageAsync(ListPageKey key)
		{
			var page = await _listService.GetPageAsync(key);

			return new Hyperlist(
				_resourceContext.CreateHeader("Issues"),
				new Hyperlink("issues?page={page}&pageSize={page-size}&sort={sort}", relationship: new Relationship("grasp:list-page")),
				key,
				page.Context,
				CreatePage(page));
		}

		private static HyperlistPage CreatePage(ListPage page)
		{
			return new HyperlistPage(page.Key.Number, page.Key.Size, page.FirstItemNumber, page.LastItemNumber, CreateItems(page));
		}

		private static HyperlistItems CreateItems(ListPage page)
		{
			return new HyperlistItems(page.Items.Schema, page.Items.Select(CreateItem));
		}

		private static HyperlistItem CreateItem(ListItem listItem)
		{
			var number = ((int) listItem["Number"]).ToString();

			var link = new Hyperlink("issues/{0}".FormatCurrent(number), listItem.Number, number, new Relationship("grasp:list-item"));

			return new HyperlistItem(link, listItem);
		}
	}
}