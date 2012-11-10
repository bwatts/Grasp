using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Cloak.Autofac;
using Grasp;
using Slate.Forms;
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

			RegisterModule(new DomainModule(typeof(Notion).Assembly, typeof(Form).Assembly));

			RegisterModule(new ApiModule(httpConfiguration, siteConfiguration));

			RegisterModule(new RavenModule(siteConfiguration.ConnectionStringName, typeof(FormListService).Assembly));
		}
	}
}