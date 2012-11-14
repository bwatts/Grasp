using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Autofac;
using Cloak.Autofac;
using Cloak.Http.Media;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Raven;
using Grasp.Hypermedia.Server;
using Grasp.Lists;

namespace Slate.Http.Server.Composition
{
	public class WorkModule : BuilderModule
	{
		public WorkModule(HttpConfiguration httpSettings)
		{
			Contract.Requires(httpSettings != null);

			var workStatusFormat = new WorkItemHtmlFormat();

			httpSettings.Formatters.Add(workStatusFormat);

			RegisterInstance(workStatusFormat).As<MediaFormat>();

			RegisterType<WorkItemListService>().Named<IListService>("Work").InstancePerDependency();

			RegisterType<WorkItemByIdQuery>().As<IWorkItemByIdQuery>().InstancePerDependency();

			Register(c => new WorkItemStore(c.Resolve<IHttpResourceContext>(), c.ResolveNamed<IListService>("Work"), c.Resolve<IWorkItemByIdQuery>()))
			.As<IWorkItemStore>()
			.InstancePerDependency();

			RegisterType<WorkController>().InstancePerDependency();

			httpSettings.Routes.MapHttpRoute("work-list", "work", new { controller = "Work" });
			httpSettings.Routes.MapHttpRoute("work-details", "work/{id}", new { controller = "Work" });
		}
	}
}