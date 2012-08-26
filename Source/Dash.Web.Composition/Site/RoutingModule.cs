using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;

namespace Dash.Web.Composition.Site
{
	public class RoutingModule : BuilderModule
	{
		public RoutingModule(RouteCollection routes)
		{
			Contract.Requires(routes != null);

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
		}
	}
}