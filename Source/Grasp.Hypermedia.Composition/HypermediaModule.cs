using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Autofac.Integration.WebApi;
using Cloak.Autofac;

namespace Grasp.Hypermedia.Http.Composition
{
	public class HypermediaModule : BuilderModule
	{
		public HypermediaModule(HttpConfiguration httpConfiguration, params Assembly[] resourceAssemblies)
		{
			Contract.Requires(httpConfiguration != null);

			// TODO: Resource types

			httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new ResourceSelector(httpConfiguration, null));

			this.RegisterHttpResources(resourceAssemblies).InstancePerApiRequest();
		}
	}
}