using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using Cloak.Autofac;

namespace Slate.Web.Server.Site
{
	public class ErrorModule : BuilderModule
	{
		public ErrorModule(GlobalFilterCollection filters)
		{
			Contract.Requires(filters != null);

			filters.Add(new HandleErrorAttribute());
		}
	}
}