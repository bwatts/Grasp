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
using Grasp.Work;
using Slate.Forms;
using Slate.Http.Api;
using Slate.Http.Persistence;
using Slate.Http.Server.Configuration;
using Slate.Services;

namespace Slate.Http.Server.Composition
{
	public class FormsModule : BuilderModule
	{
		public FormsModule(HttpConfiguration httpSettings, SiteSection siteSettings)
		{
			Contract.Requires(httpSettings != null);

			RegisterType<FormListService>().Named<IListService>("Forms").InstancePerDependency();

			Register(c => new StartFormService(siteSettings.WorkRetryInterval)).As<IStartFormService>().SingleInstance();

			Register(c => new FormsController(
				c.Resolve<IHttpResourceContext>(),
				c.ResolveNamed<IListService>("Forms"),
				c.Resolve<IStartFormService>(),
				c.Resolve<IRepository<Form>>()))
			.InstancePerDependency();

			httpSettings.Routes.MapHttpRoute("form-list", "forms", new { controller = "Forms" });
			httpSettings.Routes.MapHttpRoute("form-details", "forms/{id}", new { controller = "Forms" });
		}
	}
}