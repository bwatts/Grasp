using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using Cloak.Http;
using Grasp;
using Grasp.Hypermedia;
using Slate.Http;

namespace Slate.Web.Site.Presentation.Explore.Forms
{
	public sealed class FormService : Notion, IFormService
	{
		public static readonly Field<IApiSession> _sessionField = Field.On<FormService>.For(x => x._session);
		public static readonly Field<ApiClient> _clientField = Field.On<FormService>.For(x => x._client);

		private IApiSession _session { get { return GetValue(_sessionField); } set { SetValue(_sessionField, value); } }
		private ApiClient _client { get { return GetValue(_clientField); } set { SetValue(_clientField, value); } }

		public FormService(IApiSession session, ApiClient client)
		{
			Contract.Requires(session != null);
			Contract.Requires(client != null);

			_session = session;
			_client = client;
		}

		public async Task<FormResource> GetFormAsync(EntityId id)
		{
			// TODO: Combine session and client into single type? Might be tough because session requires Grasp.Hypermedia, but ApiClient is in Cloak.Http.

			var url = await _session.GetEntityUrlAsync("form", id.ToString());

			return await _client.SendWithResultAsync<FormResource>(
				http => http.GetAsync(url),
				(content, formats) => content.ReadAsAsync<FormResource>(formats));
		}

		public Task<WorkItemResource> StartFormAsync(string name)
		{
			// TODO: We are cheating slightly here by embedding some knowledge here in the client:
			//
			// 1. The server's URL space
			// 2. The parameters of creating a form
			//
			// A more robust, hypermedia-based approach would instead serve a <form> element which describes the fact that "name" is
			// a field that can be posted to start a form. See Grasp.Hypermedia/_Examples/api.html for a possible approach.

			return _client.SendWithResultAsync<WorkItemResource>(
				http => http.PostAsync("forms", new FormUrlEncodedContent(new Dictionary<string, string> { { "name", name } })),
				(content, formats) => content.ReadAsAsync<WorkItemResource>(formats));
		}
	}
}