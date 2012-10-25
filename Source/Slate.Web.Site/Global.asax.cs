using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cloak.Autofac;
using Cloak.Web.Mvc.Autofac;
using Slate.Web.Site.Composition;

namespace Slate.Web.Site
{
	public class Global : HttpApplication
	{
		private CompositionRoot _root;
		private MvcCompositionRoot _mvcRoot;

		protected void Application_Start()
		{
			_root = new CompositionRoot<SiteModule>();

			_mvcRoot = new MvcCompositionRoot(_root);

			_mvcRoot.Compose();
		}

		protected void Application_End()
		{
			_root.Dispose();
		}
	}
}