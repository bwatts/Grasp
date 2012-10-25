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

		public async Task<ListResource> GetListPageAsync(ListPageKey key)
		{
			var page = await _listService.GetPageAsync(key);

			var list = new ListResource(_resourceContext.CreateHeader("Forms"), key, page.Context, CreateResourcePage(page));

			Mesh.LinkField.Set(list, new HtmlLink("forms?page={page}&pageSize={page-size}&sort={sort}", relationship: new Relationship("grasp:list-page")));

			return list;
		}

		private static ListResourcePage CreateResourcePage(ListPage page)
		{
			return null;

			//return new ListResourcePage(
			//	page.Key.Number,
			//	page.Key.Size,
			//	page.FirstItem,
			//	page.LastItem,
			//	new ListResourceItems(
			//		AnonymousDictionary.Read<Type>(new
			//		{
			//			Name = typeof(string),
			//			Visibility = typeof(string),
			//			ResponseCount = typeof(int),
			//			IssueCount = typeof(int),
			//			WhenCreated = typeof(DateTime),
			//			WhenModified = typeof(DateTime)
			//		}),
			//		page.Items.Select(item =>
			//		{
			//			MLink.ValueField.Set(item, new HtmlLink("TODO", relationship: new Relationship("grasp:list-item")));

			//			return item;
			//		})
			//		.ToList()
			//		.AsReadOnly()));
		}
	}
}