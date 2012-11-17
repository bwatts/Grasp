using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Cloak.Http;
using Grasp;
using Grasp.Hypermedia;

namespace Slate.Web.Site.Presentation.Work
{
	public sealed class WorkService : Notion, IWorkService
	{
		public static readonly Field<IApiSession> _sessionField = Field.On<WorkService>.For(x => x._session);
		public static readonly Field<ApiClient> _clientField = Field.On<WorkService>.For(x => x._client);

		private IApiSession _session { get { return GetValue(_sessionField); } set { SetValue(_sessionField, value); } }
		private ApiClient _client { get { return GetValue(_clientField); } set { SetValue(_clientField, value); } }

		public WorkService(IApiSession session, ApiClient client)
		{
			Contract.Requires(session != null);
			Contract.Requires(client != null);

			_session = session;
			_client = client;
		}

		public async Task<WorkItemResource> GetWorkItemAsync(Guid id)
		{
			return await GetWorkItemAsync(await GetWorkItemUrlAsync(id));
		}

		private Task<Uri> GetWorkItemUrlAsync(Guid id)
		{
			return _session.GetEntityUrlAsync("work", id.ToString("N").ToUpper());
		}

		private Task<WorkItemResource> GetWorkItemAsync(Uri url)
		{
			return _client.SendWithResultAsync<WorkItemResource>(
				http => http.GetAsync(url),
				(content, formats) => content.ReadAsAsync<WorkItemResource>(formats));
		}
	}
}