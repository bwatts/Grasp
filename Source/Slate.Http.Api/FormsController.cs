using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Cloak;
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

		public FormsController(IFormStore formStore, IStartWorkService startWorkService, TimeSpan startRetryInterval)
		{
			Contract.Requires(formStore != null);
			Contract.Requires(startWorkService != null);
			Contract.Requires(startRetryInterval >= TimeSpan.Zero);

			_formStore = formStore;
			_startWorkService = startWorkService;
			_startRetryInterval = startRetryInterval;
		}

		[HttpGet]
		public Task<Hyperlist> GetListPageAsync(ListPageKey key)
		{
			return _formStore.GetListAsync(key);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetItemAsync(HttpRequestMessage request, Guid id)
		{
			var form = await _formStore.GetFormAsync(id);

			return form == null
				? request.CreateResponse(HttpStatusCode.NotFound)
				: request.CreateResponse(HttpStatusCode.OK, form);
		}

		[HttpPost]
		public async Task<HttpResponseMessage> StartAsync(HttpRequestMessage request, StartFormArguments arguments)
		{
			var response = request.CreateResponse(HttpStatusCode.Accepted);

			response.Headers.Location = await StartFormAsync(arguments.name);

			return response;
		}

		private Task<Uri> StartFormAsync(string name)
		{
			return _startWorkService.StartWorkAsync(
				"Start form '{0}'".FormatCurrent(name),
				_startRetryInterval,
				(workItemId, channel) => channel.IssueAsync(new StartFormCommand(workItemId, Guid.NewGuid(), name)));
		}

		public sealed class StartFormArguments
		{
			public string name { get; set; }
		}
	}
}