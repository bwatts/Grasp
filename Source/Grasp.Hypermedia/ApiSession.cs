using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cloak;
using Cloak.Http;
using Grasp.Hypermedia.Linq;

namespace Grasp.Hypermedia
{
	public sealed class ApiSession : Notion, IApiSession
	{
		public static readonly Field<ApiClient> _clientField = Field.On<ApiSession>.For(x => x._client);
		public static readonly Field<ApiResource> _apiField = Field.On<ApiSession>.For(x => x._api);

		private ApiClient _client { get { return GetValue(_clientField); } set { SetValue(_clientField, value); } }
		private ApiResource _api { get { return GetValue(_apiField); } set { SetValue(_apiField, value); } }

		private volatile bool _loading;

		public ApiSession(ApiClient client)
		{
			Contract.Requires(client != null);

			_client = client;
		}

		public async Task<Uri> GetEntitySetUrlAsync(MClass @class, bool throwIfEntityUnsupported)
		{
			await EnsureLoadedAsync();

			var entitySetLink = _api.Links.FirstOrDefault(link => link.Relationship == "grasp:entity-set" && link.Class == @class);

			if(throwIfEntityUnsupported && entitySetLink == null)
			{
				throw new HypermediaException("API does not support an entity set corresponding to class '{0}'".FormatInvariant(@class));
			}

			return entitySetLink == null ? null : entitySetLink.ToUri();
		}

		public async Task<Uri> GetEntityUrlAsync(MClass @class, string id, bool throwIfEntityUnsupported)
		{
			await EnsureLoadedAsync();

			var entityLink = _api.Links.FirstOrDefault(link => link.Relationship == "grasp:entity" && link.Class == @class);

			if(throwIfEntityUnsupported && entityLink == null)
			{
				throw new HypermediaException("API does not support entities corresponding to class '{0}'".FormatInvariant(@class));
			}

			return entityLink == null ? null : entityLink.BindHrefVariable("id", id);
		}

		private async Task EnsureLoadedAsync()
		{
			if(_api == null)
			{
				_loading = true;

				if(_loading)
				{
					_api = await LoadAsync();

					_loading = false;
				}
			}
		}

		private Task<ApiResource> LoadAsync()
		{
			return _client.SendWithResultAsync<ApiResource>(
				http => http.GetAsync(http.BaseAddress),
				(content, formats) => content.ReadAsAsync<ApiResource>(formats));
		}
	}
}