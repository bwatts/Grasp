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
using Grasp.Lists;

namespace Grasp.Hypermedia.Lists
{
	public sealed class ListClient : Notion, IListClient
	{
		public static readonly Field<ApiClient> _apiClientField = Field.On<ListClient>.For(x => x._apiClient);

		private ApiClient _apiClient { get { return GetValue(_apiClientField); } set { SetValue(_apiClientField, value); } }

		public ListClient(ApiClient apiClient)
		{
			Contract.Requires(apiClient != null);

			_apiClient = apiClient;
		}

		public Task<Hyperlist> GetListAsync(Uri uri, ListPageKey pageKey = null)
		{
			return _apiClient.SendWithResultAsync<Hyperlist>(
				http => http.GetAsync(GetApiUri(uri, pageKey)),
				(content, formats) => content.ReadAsAsync<Hyperlist>(formats));
		}

		private static Uri GetApiUri(Uri uri, ListPageKey pageKey)
		{
			return new UriBuilder(uri) { Query = GetQuery(uri, pageKey) }.Uri;
		}

		private static string GetQuery(Uri uri, ListPageKey pageKey)
		{
			return pageKey.GetQuery(includeSeparator: uri.Query.Length < 2);
		}
	}
}