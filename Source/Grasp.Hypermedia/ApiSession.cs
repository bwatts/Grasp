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
		public static readonly Field<object> _loadRootSyncField = Field.On<ApiSession>.For(x => x._loadRootSync);

		private ApiClient _client { get { return GetValue(_clientField); } set { SetValue(_clientField, value); } }
		private ApiResource _api { get { return GetValue(_apiField); } set { SetValue(_apiField, value); } }
		private object _loadRootSync { get { return GetValue(_loadRootSyncField); } set { SetValue(_loadRootSyncField, value); } }

		public ApiSession(ApiClient client)
		{
			Contract.Requires(client != null);

			_client = client;

			_loadRootSync = new object();
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

			return entityLink == null ? null : entityLink.BindUriVariable("id", id);
		}

		private async Task EnsureLoadedAsync()
		{
			if(_api == null)
			{
				var lockTaken = false;

				Monitor.Enter(_loadRootSync, ref lockTaken);

				if(lockTaken)
				{
					_api = await LoadAsync();

					Monitor.Exit(_loadRootSync);
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