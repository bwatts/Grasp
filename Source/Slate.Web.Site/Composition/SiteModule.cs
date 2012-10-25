using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cloak.Autofac;
using Slate.Web.Site.Composition.Http;
using Slate.Web.Site.Composition.UI;
using Slate.Web.Site.Configuration;

namespace Slate.Web.Site.Composition
{
	public class SiteModule : BuilderModule
	{
		public SiteModule()
		{
			var routes = RouteTable.Routes;
			var bundles = BundleTable.Bundles;
			var filters = GlobalFilters.Filters;
			var viewEngines = ViewEngines.Engines;

			RegisterInstance(routes);
			RegisterInstance(bundles);
			RegisterInstance(filters);
			RegisterInstance(viewEngines);

			var siteConfiguration = CompositionConfiguration.GetRequiredSection<SiteSection>("slate/web.site");

			RegisterModule<SecurityModule>();
			RegisterModule<TimeModule>();

			RegisterModule(new HttpModule(siteConfiguration));

			RegisterModule(new UIModule(routes, bundles, filters, viewEngines));

			RegisterModule(new HomeModule(routes));
		}
	}
}