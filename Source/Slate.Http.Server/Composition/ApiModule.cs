using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Http;
using Cloak.Autofac;
using Cloak.Http.Media;
using Grasp.Hypermedia;
using Slate.Http.Api;
using Slate.Http.Persistence;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition
{
	public class ApiModule : BuilderModule
	{
		public ApiModule(HttpConfiguration httpSettings, ServerSection serverSettings)
		{
			Contract.Requires(httpSettings != null);
			Contract.Requires(serverSettings != null);

			var apiFormat = new ApiHtmlFormat();

			RegisterInstance(apiFormat).As<MediaFormat>();

			httpSettings.Formatters.Add(apiFormat);

			RegisterModule(new ErrorModule(httpSettings, serverSettings));

			RegisterModule<MessagingModule>();

			RegisterModule(new ListModule(httpSettings));

			RegisterModule(new FormsModule(httpSettings, serverSettings));
			RegisterModule(new IssuesModule(httpSettings));

			RegisterModule(new WorkModule(httpSettings));

			Register(c => new HttpResourceContext(serverSettings.BaseUrl)).As<IHttpResourceContext>().SingleInstance();

			RegisterType<HomeController>().InstancePerDependency();

			httpSettings.Routes.MapHttpRoute("home", "", new { controller = "Home" });
		}
	}
}