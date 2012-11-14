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
using Grasp.Messaging;
using Grasp.Work.Items;

namespace Grasp.Hypermedia.Server
{
	public class WorkController : ApiController
	{
		private readonly IWorkItemStore _workItemStore;
		private readonly IMessageChannel _messageChannel;

		public WorkController(IWorkItemStore workItemStore, IMessageChannel messageChannel)
		{
			Contract.Requires(workItemStore != null);
			Contract.Requires(messageChannel != null);

			_workItemStore = workItemStore;
			_messageChannel = messageChannel;
		}

		[HttpGet]
		public Task<Hyperlist> GetListPageAsync(ListPageKey key)
		{
			return _workItemStore.GetListAsync(key);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetItemAsync(HttpRequestMessage request, Guid id)
		{
			var item = await _workItemStore.GetWorkItemAsync(id);

			return item == null
				? request.CreateResponse(HttpStatusCode.NotFound)
				: request.CreateResponse(HttpStatusCode.OK, item);
		}

		[HttpPost]
		public async Task<HttpResponseMessage> StartWorkAsync(HttpRequestMessage request, StartWorkParameters parameters)
		{
			var workItemId = Guid.NewGuid();

			await _messageChannel.IssueAsync(new StartWorkCommand(workItemId, parameters.description, parameters.retryInterval));

			var response = request.CreateResponse(HttpStatusCode.Accepted);

			response.Headers.Location = _workItemStore.GetLocation(workItemId);

			return response;
		}

		[HttpPost]
		public Task<WorkItemResource> CompleteWorkAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public sealed class StartWorkParameters
		{
			public string description { get; set; }

			public TimeSpan retryInterval { get; set; }
		}
	}
}