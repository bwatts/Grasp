using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cloak.Autofac;
using Cloak.Http.WebHost.Autofac;
using Slate.Http.Server.Composition;

namespace Slate.Http.Server
{
	public class Global : HttpApplication
	{
		private CompositionRoot _root;
		private HttpCompositionRoot _httpRoot;

		protected void Application_Start()
		{
			_root = new CompositionRoot<ServerModule>();

			_httpRoot = new HttpCompositionRoot(_root);

			_httpRoot.Compose();
		}

		protected void Application_End()
		{
			_root.Dispose();
		}
	}
}