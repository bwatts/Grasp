﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Cloak.Autofac;
using Cloak.Web.Http.Autofac;
using Grasp.Hypermedia;
using Grasp.Hypermedia.Server;
using Grasp.Lists;
using Slate.Http.Api;
using Slate.Http.Persistence;
using Slate.Http.Server.Configuration;
using Slate.Services;

namespace Slate.Http.Server.Composition.Api
{
	public class FormsModule : BuilderModule
	{
		public FormsModule(HttpConfiguration httpSettings, ServerSection serverSettings)
		{
			Contract.Requires(httpSettings != null);

			httpSettings.RegisterMediaFormat<FormHtmlFormat>(this);

			RegisterType<FormMesh>().As<IFormMesh>().SingleInstance();

			RegisterType<FormListService>().Named<IListService>("Forms").InstancePerDependency();

			RegisterType<FormByIdQuery>().As<IFormByIdQuery>().InstancePerDependency();

			Register(c => new FormStore(c.Resolve<IHttpResourceContext>(), c.ResolveNamed<IListService>("Forms"), c.Resolve<IFormByIdQuery>()))
			.As<IFormStore>()
			.InstancePerDependency();

			Register(c => new FormsController(c.Resolve<IFormStore>(), c.Resolve<IStartWorkService>(), serverSettings.WorkRetryInterval, c.Resolve<IWorkItemStore>()))
			.InstancePerDependency();

			httpSettings.Routes.MapHttpRoute("form-list", "forms", new { controller = "Forms", action = "GetListPageAsync" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get.Method) });
			httpSettings.Routes.MapHttpRoute("form-details", "forms/{id}", new { controller = "Forms", action = "GetItemAsync" });
			httpSettings.Routes.MapHttpRoute("form-start", "forms", new { controller = "Forms", action = "StartAsync" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post.Method) });
		}
	}
}