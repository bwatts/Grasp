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

		public async Task<Hyperlist> GetListPageAsync(ListViewKey key)
		{
			var list = await _listService.GetViewAsync(key);

			return new Hyperlist(
				_resourceContext.CreateHeader("Issues", "issues" + key.GetQuery(includeSeparator: true)),
				new Hyperlink("issues?page={page}&pageSize={page-size}&sort={sort}", relationship: "grasp:list-page"),
				key,
				list.Pages,
				CreateItems(list));
		}

		private static HyperlistItems CreateItems(ListView list)
		{
			return new HyperlistItems(list.Items.Total, list.Items.Schema, list.Items.Select(CreateItem));
		}

		private static HyperlistItem CreateItem(ListItem listItem)
		{
			var number = ((int) listItem["Number"]).ToString();

			var link = new Hyperlink("issues/{0}".FormatCurrent(number), listItem.Number, number, "grasp:list-item");

			return new HyperlistItem(link, listItem);
		}
	}
}