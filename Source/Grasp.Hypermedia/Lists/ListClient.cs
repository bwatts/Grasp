using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Http;
using Cloak.Http.Media;
using Grasp.Checks;

namespace Grasp.Hypermedia.Lists
{
	public sealed class ListClient : Notion, IListClient
	{
		public static readonly Field<Uri> _apiUrlField = Field.On<ListClient>.For(x => x._apiUrl);
		public static readonly Field<ApiClient> _apiClientField = Field.On<ListClient>.For(x => x._apiClient);

		private Uri _apiUrl { get { return GetValue(_apiUrlField); } set { SetValue(_apiUrlField, value); } }
		private ApiClient _apiClient { get { return GetValue(_apiClientField); } set { SetValue(_apiClientField, value); } }

		public ListClient(Uri apiUrl, ApiClient apiClient)
		{
			Contract.Requires(apiUrl != null);
			Contract.Requires(apiClient != null);

			_apiUrl = apiUrl;
			_apiClient = apiClient;
		}

		public Task<ListResource> GetListAsync(ListPageKey key = null)
		{
			return _apiClient.SendAsync<ListResource>(
				http => http.GetAsync(GetApiUri()),
				(content, formats) => content.ReadAsAsync<ListResource>(formats));
		}

		private Uri GetApiUri(ListPageKey key = null)
		{
			return new UriBuilder(_apiUrl) { Query = GetQuery(key) }.Uri;
		}

		private string GetQuery(ListPageKey key)
		{
			return key.GetQuery().ToStringWithSeparator(hasBaseQuery: _apiUrl.Query.Length > 1);
		}
	}
}