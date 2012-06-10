using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Dash.Windows.UI.Views;

namespace Dash.Windows.UI.Composition
{
	public class ShellModule : BuilderModule
	{
		public ShellModule()
		{
			RegisterType<ShellWindow>().InstancePerDependency();
		}
	}
}