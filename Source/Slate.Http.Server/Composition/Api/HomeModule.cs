using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Web.Http;
using Cloak.Autofac;
using Slate.Http.Api;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition.Api
{
	public class HomeModule : BuilderModule
	{
		public HomeModule(HttpConfiguration httpSettings)
		{
			Contract.Requires(httpSettings != null);

			RegisterType<HomeController>().InstancePerDependency();

			httpSettings.Routes.MapHttpRoute("home", "", new { controller = "Home" });
		}
	}
}