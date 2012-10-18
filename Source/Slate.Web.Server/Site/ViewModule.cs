using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using Cloak.Autofac;
using Slate.Web.Server.Site.Views;

namespace Slate.Web.Server.Site
{
	public class ViewModule : BuilderModule
	{
		public ViewModule(ViewEngineCollection viewEngines, params string[] viewLocationFormats)
		{
			Contract.Requires(viewEngines != null);

			viewEngines.Add(new SlateViewEngine(viewLocationFormats));
		}
	}
}