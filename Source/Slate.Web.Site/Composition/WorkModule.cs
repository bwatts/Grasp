using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Cloak.Autofac;
using Cloak.Http.Media;
using Cloak.Web.Http.Autofac;
using Grasp.Hypermedia;
using Slate.Web.Site.Presentation.Work;

namespace Slate.Web.Site.Composition
{
	public class WorkModule : BuilderModule
	{
		public WorkModule(RouteCollection routes, HttpConfiguration httpSettings)
		{
			Contract.Requires(routes != null);
			Contract.Requires(httpSettings != null);

			httpSettings.RegisterMediaFormat<WorkItemHtmlFormat>(this);

			RegisterType<WorkService>().As<IWorkService>().InstancePerDependency();

			RegisterType<WorkMesh>().As<IWorkMesh>().SingleInstance();

			RegisterType<WorkController>().InstancePerDependency();

			routes.MapRoute("work", "work/{id}", new { controller = "Work", action = "Item" });
		}
	}
}