using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Autofac;
using Cloak.Autofac;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Lists;
using Grasp.Lists;
using Slate.Http.Api;
using Slate.Http.Persistence;

namespace Slate.Http.Server.Composition
{
	public class FormsModule : BuilderModule
	{
		public FormsModule(HttpConfiguration configuration)
		{
			Contract.Requires(configuration != null);

			RegisterType<FormListService>().Named<IListService>("Forms").InstancePerDependency();

			Register(c => new FormsController(c.Resolve<IHttpResourceContext>(), c.ResolveNamed<IListService>("Forms"))).InstancePerDependency();

			configuration.Routes.MapHttpRoute("forms", "forms", new { controller = "Forms", action = "GetListPageAsync" });
		}
	}
}