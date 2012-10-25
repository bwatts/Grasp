using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cloak.Autofac;
using Cloak.Web.Mvc.Autofac;

namespace Slate.Web.Site.Composition.UI
{
	public class UIModule : BuilderModule
	{
		public UIModule(RouteCollection routes, BundleCollection bundles, GlobalFilterCollection filters, ViewEngineCollection viewEngines)
		{
			RegisterModule(new JQueryModule(bundles));
			RegisterModule(new ModernizrModule(bundles));
			RegisterModule(new MvcModule(filters, routes));
			RegisterModule(new ThemeModule(bundles));
			RegisterModule(new ViewModule(routes, viewEngines));
		}
	}
}