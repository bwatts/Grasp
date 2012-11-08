using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Cloak.Autofac;
using Slate.Http.Persistence;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition
{
	public class ServerModule : BuilderModule
	{
		public ServerModule()
		{
			var httpConfiguration = GlobalConfiguration.Configuration;
			var siteConfiguration = CompositionConfiguration.GetRequiredSection<SiteSection>("slate/site");

			RegisterInstance(httpConfiguration);

			RegisterModule<TimeModule>();

			RegisterModule(new RavenModule(siteConfiguration.ConnectionStringName, typeof(FormListService).Assembly));

			RegisterModule(new ApiModule(httpConfiguration, siteConfiguration));
		}
	}
}