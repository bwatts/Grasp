using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Cloak.Autofac;
using Slate.Web.Site.Presentation.Home;
using Slate.Web.Site.Presentation.Lists;
using Slate.Web.Site.Configuration;

namespace Slate.Web.Site.Composition
{
	public class HomeModule : BuilderModule
	{
		public HomeModule(RouteCollection routes, SiteSection siteSettings)
		{
			Contract.Requires(routes != null);
			Contract.Requires(siteSettings != null);

			Register(c => new IndexModelFactory(
				c.Resolve<ISnapshotModelFactory>(),
				siteSettings.FormListUrl,
				siteSettings.IssueListUrl,
				c.ResolveNamed<IListModelFactory>("Forms"),
				c.ResolveNamed<IListModelFactory>("Issues")))
			.As<IIndexModelFactory>()
			.InstancePerDependency();

			RegisterType<HomeController>().InstancePerDependency();

			routes.MapRoute("home", "", new { controller = "Home", action = "Index" });
		}
	}
}