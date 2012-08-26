using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Dash.Context;

namespace Dash.Windows
{
	public class WindowsContext : SynchronousContext
	{
		private Application _application { get; set; }
		private ICompositionRoot<Window> _compositionRoot { get; set; }
		private Window _rootWindow { get; set; }

		public WindowsContext(Application application, ICompositionRoot<Window> compositionRoot)
		{
			Contract.Requires(application != null);
			Contract.Requires(compositionRoot != null);

			_application = application;
			_compositionRoot = compositionRoot;
		}

		protected override void Execute()
		{
			CreateRootWindow();

			_application.MainWindow = _rootWindow;

			_rootWindow.ShowDialog();

			_rootWindow.Close();

			_application.MainWindow = null;
		}

		private void CreateRootWindow()
		{
			_rootWindow = _compositionRoot.Value;

			Observable
				.FromEvent<KeyEventHandler, KeyEventArgs>(
					handler => new KeyEventHandler((sender, args) => handler(args)),
					handler => _rootWindow.KeyUp += handler,
					handler => _rootWindow.KeyUp -= handler)
				.Where(args => args.Key == Key.Escape)
				.Subscribe(args => _rootWindow.Close());
		}
	}
}