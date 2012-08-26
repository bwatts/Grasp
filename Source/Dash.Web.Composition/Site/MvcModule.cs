using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;

namespace Dash.Web.Composition.Site
{
	public class MvcModule : BuilderModule
	{
		public MvcModule(GlobalFilterCollection globalFilters, RouteCollection routes, ViewEngineCollection viewEngines)
		{
			Contract.Requires(viewEngines != null);

			RegisterModule<AreaModule>();
			RegisterModule(new FilterModule(globalFilters));
			RegisterModule(new RoutingModule(routes));

			viewEngines.Add(new SiteViewEngine());
		}
	}
}