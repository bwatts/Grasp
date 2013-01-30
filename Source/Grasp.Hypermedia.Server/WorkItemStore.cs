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

		public async Task<Hyperlist> GetListAsync(HyperlistQuery query)
		{
			var list = await _listService.GetViewAsync(query.Key);

			return GetHyperlist(
				_resourceContext.CreateHeader("Work", "work" + query.GetString(includeSeparator: true)),
				query,
				list,
				item =>
				{
					var id = (EntityId) item["Id"];
					var description = (string) item["Description"];

					var link = new Hyperlink("work/" + id.ToString(), description, @class: "grasp:list-item");

					return new HyperlistItem(link, item);
				});
		}

		public async Task<WorkItemResource> GetWorkItemAsync(EntityId id)
		{
			var item = await _byIdQuery.GetWorkItemAsync(id);

			return item == null ? null : CreateResource(item);
		}

		private WorkItemResource CreateResource(WorkItem item)
		{
			var header = _resourceContext.CreateHeader(item.Description, "work/" + item.Id.ToString());

			var whenStarted = Lifetime.CreatedEventField.Get(item);

			return item.Progress < Progress.Complete
				? new WorkItemResource(header, whenStarted, Progress.Accepted, item.RetryInterval)
				: new WorkItemResource(header, whenStarted, new Hyperlink(item.ResultLocation, relationship: "grasp:work-result"));
		}
	}
}