using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cloak.Autofac;
using Slate.Web.Site.Composition.Infrastructure;
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
			var binders = ModelBinders.Binders;

			RegisterInstance(routes);
			RegisterInstance(bundles);
			RegisterInstance(filters);
			RegisterInstance(viewEngines);
			RegisterInstance(binders);

			var httpSettings = GlobalConfiguration.Configuration;
			var siteSettings = CompositionConfiguration.GetRequiredSection<SiteSection>("slate/web.site");

			RegisterInstance(httpSettings);
			RegisterInstance(siteSettings);

			RegisterModule(new InfrastructureModule(binders, httpSettings, siteSettings));
			RegisterModule(new UIModule(routes, bundles, filters, viewEngines));

			RegisterModule(new ExploreModule(routes));
			RegisterModule(new FormsModule(routes, "[No forms]"));
			RegisterModule(new HomeModule(routes, siteSettings));
			RegisterModule(new IssuesModule("[No issues - nice!]"));
			RegisterModule<SnapshotModule>();
		}
	}
}