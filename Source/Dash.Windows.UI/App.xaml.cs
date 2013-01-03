using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Cloak.Autofac;

namespace Dash.Windows.UI
{
	public partial class App : Application
	{
		private CompositionRoot _compositionRoot;

		public App()
		{
			Startup += App_Startup;
		}

		private void App_Startup(object sender, StartupEventArgs e)
		{
			Compose();

			MainWindow = ResolveShell();

			MainWindow.Show();
		}

		private void Compose()
		{
			_compositionRoot = new CompositionRoot<AppModule>();

			_compositionRoot.Compose();
		}

		private Shell ResolveShell()
		{
			return _compositionRoot.Container.Resolve<Shell>();
		}
	}
}