using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Dash.Autofac;
using Dash.Context;
using Dash.Windows.Composition;
using Dash.Windows.UI.Composition;
using Dash.Windows.UI.Views;

namespace Dash.Windows.UI
{
	public partial class App : Application
	{
		private IDashContext _appContext;

		public App()
		{
			InitializeComponent();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_appContext = DashApplication.GetContext<AppModule, ShellModule>(root => new WindowsContext(this, root));

			// TODO: Get an IHostable from MEF and execute it with _host

			// _host.Execute(system);
		}
	}
}