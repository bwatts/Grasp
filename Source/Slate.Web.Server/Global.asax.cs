using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Cloak.Autofac;

namespace Slate.Web.Server
{
	public class Global : HttpApplication
	{
		private CompositionRoot<AppModule> _compositionRoot;

		protected void Application_Start()
		{
			Compose();

			IntegrateWithMvc();

			IntegrateWithWebApi();
		}

		protected void Application_End()
		{
			_compositionRoot.Dispose();
		}

		private void Compose()
		{
			_compositionRoot = new CompositionRoot<AppModule>();

			_compositionRoot.Compose();
		}

		private void IntegrateWithMvc()
		{
			DependencyResolver.SetResolver(new AutofacDependencyResolver(_compositionRoot.Container));
		}

		private void IntegrateWithWebApi()
		{
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_compositionRoot.Container);
		}
	}
}