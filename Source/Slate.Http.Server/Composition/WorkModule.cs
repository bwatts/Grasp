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
using Grasp.Work;
using Grasp.Work.Items;
using Slate.Http.Api;
using Slate.Http.Server.Configuration;

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

			Register(c => new WorkController(c.Resolve<IHttpResourceContext>(), c.Resolve<IRepository<WorkItem>>())).InstancePerDependency();

			httpSettings.Routes.MapHttpRoute("work", "work/{id}", new { controller = "Work" });
		}
	}
}