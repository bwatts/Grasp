using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cloak.Autofac;
using Slate.Web.Server.Api;
using Slate.Web.Server.Site;

namespace Slate.Web.Server
{
	public class AppModule : BuilderModule
	{
		public AppModule()
		{
			RegisterModule(new ApiModule(GlobalConfiguration.Configuration));
			RegisterModule(new SiteModule(RouteTable.Routes, BundleTable.Bundles, GlobalFilters.Filters, ViewEngines.Engines, "~/Site/Views/{1}.cshtml"));
		}
	}
}