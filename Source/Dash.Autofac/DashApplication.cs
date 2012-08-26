using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;
using Dash.Context;

namespace Dash.Autofac
{
	public sealed class DashApplication
	{
		public static IDashContext GetContext<TModule, TRoot>(Func<ILifetimeScope, TRoot> resolveRoot, Func<ICompositionRoot<TRoot>, IDashContext> createContext) where TModule : IModule, new()
		{
			Contract.Requires(resolveRoot != null);
			Contract.Requires(createContext != null);

			var root = new TModule().GetCompositionRoot(resolveRoot);

			return createContext(root);
		}

		public static IDashContext GetContext<TModule, TRoot>(Func<ICompositionRoot<TRoot>, IDashContext> createContext) where TModule : IModule, new()
		{
			return GetContext<TModule, TRoot>(c => c.Resolve<TRoot>(), createContext);
		}
	}
}