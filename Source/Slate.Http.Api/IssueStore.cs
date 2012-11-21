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
	public sealed class IssueStore : HttpResourceStore, IIssueStore
	{
		public static readonly Field<IHttpResourceContext> _resourceContextField = Field.On<IssueStore>.For(x => x._resourceContext);
		public static readonly Field<IListService> _listServiceField = Field.On<IssueStore>.For(x => x._listService);

		private IHttpResourceContext _resourceContext { get { return GetValue(_resourceContextField); } set { SetValue(_resourceContextField, value); } }
		private IListService _listService { get { return GetValue(_listServiceField); } set { SetValue(_listServiceField, value); } }

		public IssueStore(IHttpResourceContext resourceContext, IListService listService)
		{
			Contract.Requires(resourceContext != null);
			Contract.Requires(listService != null);

			_resourceContext = resourceContext;
			_listService = listService;
		}

		public async Task<Hyperlist> GetListAsync(HyperlistQuery query)
		{
			var list = await _listService.GetViewAsync(query.Key);

			return GetHyperlist(
				_resourceContext.CreateHeader("Issues", "issues" + query.GetQueryString(includeSeparator: true)),
				query,
				list,
				item =>
				{
					var id = (EntityId) item["Id"];
					var name = (string) item["Title"];

					var link = new Hyperlink("issues/" + id.ToString(), name, name, "grasp:list-item", "issue");

					return new HyperlistItem(link, item);
				});
		}
	}
}