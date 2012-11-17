using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;
using Grasp.Work.Items;
using Slate.Forms;

namespace Slate.Http.Api
{
	public sealed class FormStore : HttpResourceStore, IFormStore
	{
		public static readonly Field<IHttpResourceContext> _resourceContextField = Field.On<FormStore>.For(x => x._resourceContext);
		public static readonly Field<IListService> _listServiceField = Field.On<FormStore>.For(x => x._listService);
		public static readonly Field<IFormByIdQuery> _byIdQueryField = Field.On<FormStore>.For(x => x._byIdQuery);

		private IHttpResourceContext _resourceContext { get { return GetValue(_resourceContextField); } set { SetValue(_resourceContextField, value); } }
		private IListService _listService { get { return GetValue(_listServiceField); } set { SetValue(_listServiceField, value); } }
		private IFormByIdQuery _byIdQuery { get { return GetValue(_byIdQueryField); } set { SetValue(_byIdQueryField, value); } }

		public FormStore(IHttpResourceContext resourceContext, IListService listService, IFormByIdQuery byIdQuery)
		{
			Contract.Requires(resourceContext != null);
			Contract.Requires(listService != null);
			Contract.Requires(byIdQuery != null);

			_resourceContext = resourceContext;
			_listService = listService;
			_byIdQuery = byIdQuery;
		}

		public async Task<Hyperlist> GetListAsync(ListPageKey pageKey)
		{
			var page = await _listService.GetPageAsync(pageKey);

			return GetList(
				_resourceContext.CreateHeader("Forms", "forms" + pageKey.GetQuery(includeSeparator: true)),
				new Hyperlink("forms?page={page}&pageSize={page-size}&sort={sort}", relationship: "grasp:list-page"),
				pageKey,
				page,
				item =>
				{
					var id = (Guid) item["Id"];
					var name = (string) item["Name"];

					var link = new Hyperlink("forms/" + id.ToString("N").ToUpper(), item.Number, name, "grasp:list-item", "form");

					return new HyperlistItem(link, item);
				});
		}

		public async Task<FormResource> GetFormAsync(Guid id)
		{
			var item = await _byIdQuery.GetFormAsync(id);

			return item == null ? null : CreateResource(item);
		}

		public Uri GetLocation(Guid id)
		{
			return _resourceContext.GetAbsoluteUrl(GetUrl(id));
		}

		private static string GetUrl(Guid id)
		{
			return "forms/" + id.ToString("N").ToUpper();
		}

		private FormResource CreateResource(Form form)
		{
			return new FormResource(_resourceContext.CreateHeader(form.Name, GetUrl(form.Id)), form.Id, form.Name);
		}
	}
}