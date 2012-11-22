using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Cloak.Autofac;
using Grasp.Hypermedia;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition.Api
{
	public class ResourceModule : BuilderModule
	{
		public ResourceModule(ServerSection serverSettings)
		{
			Contract.Requires(serverSettings != null);

			Register(c => new HttpResourceContext(serverSettings.BaseUrl)).As<IHttpResourceContext>().SingleInstance();
		}
	}
}