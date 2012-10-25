using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;
using Slate.Web.Site.Views;

namespace Slate.Web.Site.Composition.UI
{
	public class ViewModule : BuilderModule
	{
		public ViewModule(RouteCollection routes, ViewEngineCollection viewEngines)
		{
			Contract.Requires(viewEngines != null);

			viewEngines.Add(new SlateViewEngine());

			RegisterModule<LayoutModule>();
		}
	}
}