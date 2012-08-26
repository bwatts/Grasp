using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;
using Microsoft.Web.Optimization;

namespace Dash.Web.Composition.Site
{
	public class SiteModule : BuilderModule
	{
		public SiteModule(BundleCollection bundles, GlobalFilterCollection globalFilters, RouteCollection routes, ViewEngineCollection viewEngines)
		{
			RegisterModule(new JQueryModule(bundles));
			RegisterModule(new ModernizrModule(bundles));
			RegisterModule(new MvcModule(globalFilters, routes, viewEngines));
			RegisterModule(new ThemeModule(bundles));
		}
	}
}