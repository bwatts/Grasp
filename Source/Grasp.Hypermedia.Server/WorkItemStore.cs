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

namespace Grasp.Hypermedia.Server
{
	public sealed class WorkItemStore : HttpResourceStore, IWorkItemStore
	{
		public static readonly Field<IHttpResourceContext> _resourceContextField = Field.On<WorkItemStore>.For(x => x._resourceContext);
		public static readonly Field<IListService> _listServiceField = Field.On<WorkItemStore>.For(x => x._listService);
		public static readonly Field<IWorkItemByIdQuery> _byIdQueryField = Field.On<WorkItemStore>.For(x => x._byIdQuery);

		private IHttpResourceContext _resourceContext { get { return GetValue(_resourceContextField); } set { SetValue(_resourceContextField, value); } }
		private IListService _listService { get { return GetValue(_listServiceField); } set { SetValue(_listServiceField, value); } }
		private IWorkItemByIdQuery _byIdQuery { get { return GetValue(_byIdQueryField); } set { SetValue(_byIdQueryField, value); } }

		public WorkItemStore(IHttpResourceContext resourceContext, IListService listService, IWorkItemByIdQuery byIdQuery)
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
				_resourceContext.CreateHeader("Work"),
				new Hyperlink("work?page={page}&pageSize={page-size}&sort={sort}", relationship: "grasp:list-page"),
				pageKey,
				page,
				item =>
				{
					var id = (Guid) item["Id"];
					var description = (string) item["Description"];

					var link = new Hyperlink("work/" + id.ToString("N").ToUpper(), description, @class: "grasp:list-item");

					return new HyperlistItem(link, item);
				});
		}

		public async Task<WorkItemResource> GetWorkItemAsync(Guid id)
		{
			var item = await _byIdQuery.GetWorkItemAsync(id);

			return item == null ? null : CreateResource(item);
		}

		public Uri GetLocation(Guid id)
		{
			return _resourceContext.GetAbsoluteUrl("work/" + id.ToString("N").ToUpper());
		}

		private WorkItemResource CreateResource(WorkItem item)
		{
			var header = _resourceContext.CreateHeader(item.Description);

			if(item.Progress == Progress.Accepted)
			{
				return new WorkItemResource(header, item.Id, "Accepted", item.RetryInterval);
			}
			else if(item.Progress == Progress.Complete)
			{
				return new WorkItemResource(header, item.Id, "Complete", new Hyperlink(item.ResultResource, relationship: "grasp:work-result"));
			}
			else
			{
				return new WorkItemResource(header, item.Id, "In progress", item.RetryInterval, item.Progress);
			}
		}
	}
}