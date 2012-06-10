using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Autofac;
using Autofac.Core;

namespace Dash.Windows.Composition
{
	public sealed class Shell<TWindow> where TWindow : Window
	{
		private readonly Func<IModule> _rootModuleFunction;
		private Application _application;
		private ILifetimeScope _rootLifetimeScope;
		private Window _rootWindow;

		public Shell(Func<IModule> rootModuleFunction)
		{
			Contract.Requires(rootModuleFunction != null);

			_rootModuleFunction = rootModuleFunction;
		}

		public void Start(Application application)
		{
			Contract.Requires(application != null);

			if(_application != null)
			{
				throw new InvalidOperationException("This shell has already started an application");
			}

			_application = application;

			StartApplication();
		}

		public void Stop()
		{
			_rootWindow.Close();
			_rootWindow = null;

			_rootLifetimeScope.Dispose();
			_rootLifetimeScope = null;

			_application = null;
		}

		private void StartApplication()
		{
			BuildRootLifetimeScope();

			BuildRootWindow();

			_rootWindow.Show();
		}

		private void BuildRootLifetimeScope()
		{
			var builder = new ContainerBuilder();

			builder.RegisterModule(_rootModuleFunction());

			_rootLifetimeScope = builder.Build();
		}

		private void BuildRootWindow()
		{
			_rootWindow = _rootLifetimeScope.Resolve<TWindow>();

			MakeRootWindowCloseOnEscape();
		}

		private void MakeRootWindowCloseOnEscape()
		{
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