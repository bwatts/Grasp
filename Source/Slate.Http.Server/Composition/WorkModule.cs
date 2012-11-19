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
using Grasp.Work.Items;

namespace Slate.Http.Server.Composition
{
	public class WorkModule : BuilderModule
	{
		public WorkModule(HttpConfiguration httpSettings)
		{
			Contract.Requires(httpSettings != null);

			var workItemFormat = new WorkItemHtmlFormat();

			httpSettings.Formatters.Add(workItemFormat);

			RegisterInstance(workItemFormat).As<MediaFormat>();

			RegisterType<StartWorkService>().As<IStartWorkService>().InstancePerDependency();

			RegisterType<WorkItemListService>().Named<IListService>("Work").InstancePerDependency();

			RegisterType<WorkItemByIdQuery>().As<IWorkItemByIdQuery>().InstancePerDependency();

			Register(c => new WorkItemStore(c.Resolve<IHttpResourceContext>(), c.ResolveNamed<IListService>("Work"), c.Resolve<IWorkItemByIdQuery>()))
			.As<IWorkItemStore>()
			.InstancePerDependency();

			RegisterType<WorkController>().InstancePerDependency();

			httpSettings.Routes.MapHttpRoute("work-list", "work", new { controller = "Work", action = "GetListPageAsync" });
			httpSettings.Routes.MapHttpRoute("work-details", "work/{id}", new { controller = "Work", action = "GetItemAsync" });
		}
	}
}