using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Cloak;
using Grasp;
using Grasp.Hypermedia.Lists;
using Grasp.Hypermedia.Server;
using Grasp.Lists;
using Grasp.Messaging;
using Grasp.Work.Items;
using Slate.Forms;

namespace Slate.Http.Api
{
	public class FormsController : ApiController
	{
		private readonly IFormStore _formStore;
		private readonly IStartWorkService _startWorkService;
		private readonly TimeSpan _startRetryInterval;
		private readonly IWorkItemStore _workItemStore;

		public FormsController(IFormStore formStore, IStartWorkService startWorkService, TimeSpan startRetryInterval, IWorkItemStore workItemStore)
		{
			Contract.Requires(formStore != null);
			Contract.Requires(startWorkService != null);
			Contract.Requires(startRetryInterval >= TimeSpan.Zero);
			Contract.Requires(workItemStore != null);

			_formStore = formStore;
			_startWorkService = startWorkService;
			_startRetryInterval = startRetryInterval;
			_workItemStore = workItemStore;
		}

		[HttpGet]
		public Task<Hyperlist> GetListPageAsync(ListPageKey key)
		{
			return _formStore.GetListAsync(key);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetItemAsync(HttpRequestMessage request, EntityId id)
		{
			var form = await _formStore.GetFormAsync(id);

			return form == null
				? request.CreateResponse(HttpStatusCode.NotFound)
				: request.CreateResponse(HttpStatusCode.OK, form);
		}

		[HttpPost]
		public async Task<HttpResponseMessage> StartAsync(HttpRequestMessage request, StartFormArguments arguments)
		{
			var workItemId = await StartFormAsync(arguments.name);

			var workItem = await _workItemStore.GetWorkItemAsync(workItemId);

			var response = request.CreateResponse(HttpStatusCode.Accepted, workItem);

			response.Headers.Location = workItem.Header.SelfLink.ToUri();

			return response;
		}

		private Task<EntityId> StartFormAsync(string name)
		{
			return _startWorkService.StartWorkAsync(
				"Start form '{0}'".FormatCurrent(name),
				_startRetryInterval,
				(workItemId, channel) => channel.IssueAsync(new StartFormCommand(workItemId, EntityId.Generate(), name)));
		}

		public sealed class StartFormArguments
		{
			public string name { get; set; }
		}
	}
}