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
		public ApiModule(HttpConfiguration httpSettings, SiteSection siteSettings)
		{
			RegisterModule(new ListModule(httpSettings));

			RegisterModule(new FormsModule(httpSettings, siteSettings));
			RegisterModule(new IssuesModule(httpSettings));
			RegisterModule(new WorkModule(httpSettings));

			Register(c => new HttpResourceContext(siteSettings.BaseUrl)).As<IHttpResourceContext>().SingleInstance();
		}
	}
}