using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cloak.Autofac;

namespace Slate.Web.Server.Site
{
	public class SiteModule : BuilderModule
	{
		public SiteModule(RouteCollection routes, BundleCollection bundles, GlobalFilterCollection filters, ViewEngineCollection viewEngines, params string[] viewLocationFormats)
		{
			RegisterModule<AreaModule>();
			RegisterModule(new ErrorModule(filters));
			RegisterModule(new JQueryModule(bundles));
			RegisterModule(new ModernizrModule(bundles));
			RegisterModule(new RoutingModule(routes));
			RegisterModule(new ThemeModule(bundles));
			RegisterModule(new ViewModule(viewEngines, viewLocationFormats));
		}
	}
}