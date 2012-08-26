using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dash.Autofac;
using Dash.Context;

namespace Dash.Web.Server
{
	public class WebApiApplication : HttpApplication
	{
		private IDashContext _appContext;

		protected void Application_Start()
		{
			_appContext = DashApplication.GetContext<ServerModule, TODO>(root => new WebContext(root));
		}


		internal class TODO {}


	}
}