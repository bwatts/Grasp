using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak.Autofac;
using Dash.Windows.Composition;
using Dash.Windows.UI.Views;

namespace Dash.Windows.UI.Composition
{
	public class AppModule : BuilderModule
	{
		public AppModule()
		{
			RegisterModule<DashModule>();
			RegisterModule<ShellModule>();
		}
	}
}