using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Cloak;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;
using Grasp.Work;
using Slate.Forms;
using Slate.Services;

namespace Slate.Http.Api
{
	public class FormsController : ApiController
	{
		private readonly IHttpResourceContext _resourceContext;
		private readonly IListService _listService;
		private readonly IStartFormService _startFormService;
		private readonly IRepository<Form> _formRepository;

		public FormsController(IHttpResourceContext resourceContext, IListService listService, IStartFormService startFormService, IRepository<Form> formRepository)
		{
			Contract.Requires(resourceContext != null);
			Contract.Requires(listService != null);
			Contract.Requires(startFormService != null);
			Contract.Requires(formRepository != null);

			_resourceContext = resourceContext;
			_listService = listService;
			_startFormService = startFormService;
			_formRepository = formRepository;
		}

		[HttpGet]
		public async Task<Hyperlist> GetListPageAsync(ListPageKey key)
		{
			var page = await _listService.GetPageAsync(key);

			return new Hyperlist(
				_resourceContext.CreateHeader("Forms"),
				new Hyperlink("forms?page={page}&pageSize={page-size}&sort={sort}", relationship: "grasp:list-page"),
				key,
				page.Context,
				CreatePage(page));
		}

		[HttpPost]
		public async Task<HttpResponseMessage> StartAsync(HttpRequestMessage request, string name)
		{
			var workItem = await _startFormService.StartFormAsync(name);

			return request.CreateResponse(HttpStatusCode.Accepted, workItem);
		}

		

		//[HttpGet]
		//public async Task<object> GetDetailsAsync(Guid id)
		//{
			
		//}



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
			var name = (string) listItem["Name"];

			var link = new Hyperlink("forms/{0}".FormatCurrent(name), listItem.Number, name, "grasp:list-item");

			return new HyperlistItem(link, listItem);
		}
	}
}