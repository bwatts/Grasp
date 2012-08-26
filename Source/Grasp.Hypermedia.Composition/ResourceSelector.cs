using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Grasp.Hypermedia.Http.Composition
{
	public sealed class ResourceSelector : DefaultHttpControllerSelector
	{
		private readonly IEnumerable<Type> _resourceTypes;

		public ResourceSelector(HttpConfiguration configuration, IEnumerable<Type> resourceTypes) : base(configuration)
		{
			Contract.Requires(resourceTypes != null);

			_resourceTypes = resourceTypes;
		}

		public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
		{
			return GetApiController(request) ?? base.SelectController(request);
		}

		private HttpControllerDescriptor GetApiController(HttpRequestMessage request)
		{
			// TODO

			throw new NotImplementedException();
		}
	}
}