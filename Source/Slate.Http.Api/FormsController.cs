using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Cloak.Http.Media;
using Cloak.Reflection;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Linq;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Slate.Http.Api
{
	public class FormsController : ApiController
	{
		private readonly IHttpResourceContext _resourceContext;
		private readonly IListService _listService;

		public FormsController(IHttpResourceContext resourceContext, IListService listService)
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
				_resourceContext.CreateHeader("Forms"),
				new Hyperlink("forms?page={page}&pageSize={page-size}&sort={sort}", relationship: new Relationship("grasp:list-page")),
				key,
				page.Context,
				CreatePage(page));
		}

		private static HyperlistPage CreatePage(ListPage page)
		{
			return new HyperlistPage(page.Key.Number, page.Key.Size, page.FirstItemNumber, page.LastItemNumber, page.Items.Select(CreateItem));
		}

		private static HyperlistItem CreateItem(ListItem listItem)
		{
			return new HyperlistItem(new Hyperlink(null, relationship: new Relationship("grasp:list-item")));
		}
	}
}