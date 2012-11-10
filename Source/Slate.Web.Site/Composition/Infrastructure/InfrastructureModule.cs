using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Cloak.Autofac;
using Cloak.NaturalText;
using Cloak.Time;
using Slate.Web.Site.Configuration;

namespace Slate.Web.Site.Composition.Infrastructure
{
	public class InfrastructureModule : BuilderModule
	{
		public InfrastructureModule(ModelBinderDictionary binders, HttpConfiguration httpSettings, SiteSection siteSettings)
		{
			RegisterModule(new ApiClientModule(siteSettings));
			RegisterModule(new ListModule(binders, httpSettings));
			RegisterModule<MediaModule>();
			RegisterModule<SecurityModule>();
			RegisterModule<TimeModule>();
		}
	}
}