using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Dash.Windows.Composition;
using Dash.Windows.UI.Composition;
using Dash.Windows.UI.Views;

namespace Dash.Windows.UI
{
	public partial class App : Application
	{
		private Shell<ShellWindow> _shell;

		public App()
		{
			InitializeComponent();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_shell = new Shell<ShellWindow>(() => new AppModule());

			_shell.Start(this);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			_shell.Stop();

			base.OnExit(e);
		}
	}
}