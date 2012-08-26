using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cloak.Autofac;

namespace Dash.Web.Composition.Site
{
	public class AreaModule : BuilderModule
	{
		public AreaModule()
		{
			AreaRegistration.RegisterAllAreas();
		}
	}
}