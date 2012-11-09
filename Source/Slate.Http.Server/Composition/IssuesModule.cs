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
	public class IssuesModule : BuilderModule
	{
		public IssuesModule(HttpConfiguration configuration)
		{
			Contract.Requires(configuration != null);

			RegisterType<IssueListService>().Named<IListService>("Issues").InstancePerDependency();

			Register(c => new IssuesController(c.Resolve<IHttpResourceContext>(), c.ResolveNamed<IListService>("Issues"))).InstancePerDependency();

			configuration.Routes.MapHttpRoute("issues", "issues", new { controller = "Issues", action = "GetListPageAsync" });
		}
	}
}