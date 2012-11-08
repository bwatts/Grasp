using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Autofac;
using Cloak.Autofac;
using Cloak.Http;
using Cloak.Http.Media;
using Slate.Web.Site.Configuration;

namespace Slate.Web.Site.Composition
{
	public class ApiClientModule : BuilderModule
	{
		public ApiClientModule(SiteSection siteConfiguration)
		{
			RegisterType<HttpClient>().InstancePerDependency();

			Register(c => new ApiClient(siteConfiguration.ApiBaseAddress, c.Resolve<Func<HttpClient>>(), c.Resolve<MediaFormats>())).InstancePerDependency();
		}
	}
}