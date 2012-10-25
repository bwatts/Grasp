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

namespace Slate.Web.Site.Composition
{
	public class HomeModule : BuilderModule
	{
		public HomeModule(RouteCollection routes)
		{
			Contract.Requires(routes != null);

			Register(c => new IndexFactory(
				c.Resolve<ISnapshotFactory>(),
				c.ResolveNamed<IListFactory>("Forms"),
				c.ResolveNamed<IListFactory>("Issues")))
			.As<IIndexFactory>()
			.InstancePerDependency();

			RegisterType<HomeController>().InstancePerDependency();

			routes.MapRoute("home", "", new { controller = "Home", action = "Index" });
		}
	}
}