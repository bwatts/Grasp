using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using Cloak.Autofac;

namespace Slate.Web.Server.Site
{
	public class AreaModule : BuilderModule
	{
		public AreaModule()
		{
			AreaRegistration.RegisterAllAreas();
		}
	}
}