using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Http;
using Cloak.Autofac;
using Cloak.Http.Media;
using Cloak.Web.Http.Autofac;
using Grasp.Hypermedia;
using Slate.Http.Api;
using Slate.Http.Persistence;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition.Api
{
	public class ApiModule : BuilderModule
	{
		public ApiModule(HttpConfiguration httpSettings, ServerSection serverSettings)
		{
			Contract.Requires(httpSettings != null);
			Contract.Requires(serverSettings != null);

			httpSettings.RegisterMediaFormat<ApiHtmlFormat>(this);

			RegisterModule(new ContentNegotiationModule(httpSettings));
			RegisterModule(new ErrorModule(httpSettings, serverSettings));
			RegisterModule(new FormsModule(httpSettings, serverSettings));
			RegisterModule(new HomeModule(httpSettings));
			RegisterModule(new IssuesModule(httpSettings));
			RegisterModule(new ListModule(httpSettings));
			RegisterModule<MessagingModule>();
			RegisterModule(new ResourceModule(serverSettings));
			RegisterModule(new WorkModule(httpSettings));
		}
	}
}