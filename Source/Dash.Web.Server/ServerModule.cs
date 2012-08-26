using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cloak.Autofac;
using Dash.Web.Composition.Api;
using Dash.Web.Composition.Site;

namespace Dash.Web.Server
{
	public class ServerModule : BuilderModule
	{
		public ServerModule()
		{
			var httpConfiguration = GlobalConfiguration.Configuration;
			var bundles = BundleTable.Bundles;
			var globalFilters = GlobalFilters.Filters;
			var routes = RouteTable.Routes;

			var resourceAssemblies = AppDomain.CurrentDomain.GetAssemblies();

			RegisterModule(new ApiModule(httpConfiguration, routes, resourceAssemblies));
			RegisterModule(new SiteModule(bundles, globalFilters, routes));
		}
	}
}