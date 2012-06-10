using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Dash.Windows.Presentation;

namespace Dash.Windows.Composition
{
	public class DashModule : BuilderModule
	{
		public DashModule()
		{
			RegisterType<DashModel>().InstancePerDependency();
		}
	}
}