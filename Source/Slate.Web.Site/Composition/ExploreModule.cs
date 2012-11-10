using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;
using Slate.Web.Site.Presentation.Explore;

namespace Slate.Web.Site.Composition
{
	public class ExploreModule : BuilderModule
	{
		public ExploreModule(RouteCollection routes)
		{
			Contract.Requires(routes != null);

			RegisterType<ExploreController>().InstancePerDependency();

			routes.MapRoute("explore", "explore", new { controller = "Explore", action = "Index" });
		}
	}
}