using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Http;
using Cloak.Autofac;
using Grasp.Hypermedia;
using Slate.Http.Persistence;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition
{
	public class ApiModule : BuilderModule
	{
		public ApiModule(HttpConfiguration httpConfiguration, ServerSection serverConfiguration)
		{
			RegisterModule(new FormsModule(httpConfiguration));
			RegisterModule<IssuesModule>();
			RegisterModule(new ListModule(httpConfiguration));

			Register(c => new HttpResourceContext(serverConfiguration.BaseUrl)).As<IHttpResourceContext>().SingleInstance();
		}
	}
}