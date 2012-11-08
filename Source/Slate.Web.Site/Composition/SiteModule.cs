using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cloak.Autofac;
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

			RegisterModule(new UIModule(routes, bundles, filters, viewEngines));

			var httpConfiguration = GlobalConfiguration.Configuration;
			var siteConfiguration = CompositionConfiguration.GetRequiredSection<SiteSection>("slate/web.site");

			RegisterModule(new ApiClientModule(siteConfiguration));
			RegisterModule(new FormsModule("[No forms]"));
			RegisterModule(new HomeModule(routes, siteConfiguration));
			RegisterModule(new IssuesModule("[No issues - nice!]"));
			RegisterModule(new ListModule(binders, httpConfiguration));
			RegisterModule<MediaModule>();
			RegisterModule<SecurityModule>();
			RegisterModule<SnapshotModule>();
			RegisterModule<TimeModule>();
		}
	}
}