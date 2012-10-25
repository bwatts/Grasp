using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Cloak.Autofac;
using Cloak.Http;
using Slate.Web.Site.Configuration;

namespace Slate.Web.Site.Composition.Http
{
	public class ClientModule : BuilderModule
	{
		public ClientModule(SiteSection siteConfiguration)
		{
			Register(c => new HttpClient { BaseAddress = siteConfiguration.ApiBaseUrl }).InstancePerDependency();

			RegisterType<ApiClient>().InstancePerDependency();
		}
	}
}