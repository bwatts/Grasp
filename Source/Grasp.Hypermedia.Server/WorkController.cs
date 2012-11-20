using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;

namespace Grasp.Hypermedia.Server
{
	public class WorkController : ApiController
	{
		private readonly IWorkItemStore _workItemStore;

		public WorkController(IWorkItemStore workItemStore)
		{
			Contract.Requires(workItemStore != null);

			_workItemStore = workItemStore;
		}

		[HttpGet]
		public Task<Hyperlist> GetListPageAsync(ListViewKey key)
		{
			return _workItemStore.GetListAsync(key);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetItemAsync(HttpRequestMessage request, EntityId id)
		{
			var item = await _workItemStore.GetWorkItemAsync(id);

			return item == null
				? request.CreateResponse(HttpStatusCode.NotFound)
				: request.CreateResponse(HttpStatusCode.OK, item);
		}

		[HttpPost]
		public Task<WorkItemResource> CompleteWorkAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}