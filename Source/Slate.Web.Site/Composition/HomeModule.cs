using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Cloak.Autofac;
using Slate.Web.Presentation.Home;
using Slate.Web.Presentation.Lists;
using Slate.Web.Site.Configuration;

namespace Slate.Web.Site.Composition
{
	public class HomeModule : BuilderModule
	{
		public HomeModule(RouteCollection routes, SiteSection siteConfiguration)
		{
			Contract.Requires(routes != null);
			Contract.Requires(siteConfiguration != null);

			Register(c => new IndexFactory(
				c.Resolve<ISnapshotFactory>(),
				siteConfiguration.FormListUrl,
				siteConfiguration.IssueListUrl,
				c.ResolveNamed<IListFactory>("Forms"),
				c.ResolveNamed<IListFactory>("Issues")))
			.As<IIndexFactory>()
			.InstancePerDependency();

			RegisterType<HomeController>().InstancePerDependency();

			routes.MapRoute("home", "", new { controller = "Home", action = "Index" });
		}
	}
}