using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cloak.Autofac;

namespace Dash.Web.Composition.Site
{
	public class FilterModule : BuilderModule
	{
		public FilterModule(GlobalFilterCollection globalFilters)
		{
			globalFilters.Add(new HandleErrorAttribute());
		}
	}
}