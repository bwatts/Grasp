using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Routing;
using Cloak.Autofac;
using Grasp.Hypermedia.Http.Composition;

namespace Dash.Web.Composition.Api
{
	public class ApiModule : BuilderModule
	{
		public ApiModule(HttpConfiguration httpConfiguration, HttpRouteCollection routes, params Assembly[] resourceAssemblies)
		{
			RegisterModule(new HypermediaModule(httpConfiguration, resourceAssemblies));

			// TODO: This will do for now, but in future consider whether the resource has a route defined

			routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional });
		}
	}
}