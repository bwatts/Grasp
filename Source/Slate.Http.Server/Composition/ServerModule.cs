using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Cloak.Autofac;
using Grasp;
using Grasp.Raven;
using Slate.Forms;
using Slate.Http.Persistence;
using Slate.Http.Server.Configuration;

namespace Slate.Http.Server.Composition
{
	public class ServerModule : BuilderModule
	{
		public ServerModule()
		{
			var httpSettings = GlobalConfiguration.Configuration;
			var siteSettings = CompositionConfiguration.GetRequiredSection<SiteSection>("slate/site");

			RegisterInstance(httpSettings);

			RegisterModule<TimeModule>();

			RegisterModule(new DomainModule(typeof(Notion).Assembly, typeof(Form).Assembly));

			RegisterModule(new ApiModule(httpSettings, siteSettings));

			RegisterModule(new RavenModule(siteSettings.ConnectionStringName, typeof(RavenContext).Assembly, typeof(FormListService).Assembly));
		}
	}
}