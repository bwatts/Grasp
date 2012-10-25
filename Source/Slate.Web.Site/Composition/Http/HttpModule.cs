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
	public class HttpModule : BuilderModule
	{
		public HttpModule(SiteSection siteConfiguration)
		{
			RegisterModule(new ClientModule(siteConfiguration));
			RegisterModule<ListModule>();
			RegisterModule<MediaModule>();
		}
	}
}