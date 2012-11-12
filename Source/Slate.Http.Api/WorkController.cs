using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Grasp.Hypermedia;
using Grasp.Work;
using Grasp.Work.Items;

namespace Slate.Http.Api
{
	public class WorkController : ApiController
	{
		private readonly IHttpResourceContext _resourceContext;
		private readonly IRepository<WorkItem> _workItemRepository;

		public WorkController(IHttpResourceContext resourceContext, IRepository<WorkItem> workItemRepository)
		{
			Contract.Requires(resourceContext != null);
			Contract.Requires(workItemRepository != null);

			_resourceContext = resourceContext;
			_workItemRepository = workItemRepository;
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetItemAsync(HttpRequestMessage request, Guid id)
		{
			var item = await _workItemRepository.LoadAsync(id);

			return item == null
				? request.CreateResponse(HttpStatusCode.NotFound)
				: request.CreateResponse(HttpStatusCode.OK, GetItemResource(id, item));
		}

		private WorkItemResource GetItemResource(Guid id, WorkItem item)
		{
			var header = _resourceContext.CreateHeader(item.Description);

			if(item.Progress == Progress.Accepted)
			{
				return new WorkItemResource(header, id, "Accepted", item.RetryInterval);
			}
			else if(item.Progress == Progress.Complete)
			{
				return new WorkItemResource(header, id, "Complete", new Hyperlink(item.ResultResource, relationship: "grasp:work-result"));
			}
			else
			{
				return new WorkItemResource(header, id, "In progress", item.RetryInterval, item.Progress);
			}
		}
	}
}