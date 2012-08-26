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
	// TODO: Put these in Cloak to avoid defining a new Autofac extension

	public static class AutofacCompositionRoot
	{
		public static ICompositionRoot<T> GetCompositionRoot<T>(this IModule module, Func<ILifetimeScope, T> resolveValue)
		{
			return new AutofacCompositionRoot<T>(module, resolveValue);
		}
	}

	public sealed class AutofacCompositionRoot<T> : CompositionRoot<T>
	{
		private IModule _rootModule { get; set; }
		private Func<ILifetimeScope, T> _resolveValue { get; set; }
		private ILifetimeScope _rootLifetimeScope { get; set; }

		public AutofacCompositionRoot(IModule rootModule, Func<ILifetimeScope, T> resolveValue)
		{
			Contract.Requires(rootModule != null);
			Contract.Requires(resolveValue != null);

			_rootModule = rootModule;
			_resolveValue = resolveValue;
		}

		public AutofacCompositionRoot(IModule rootModule) : this(rootModule, c => c.Resolve<T>())
		{}

		protected override T ResolveValue()
		{
			BuildRootLifetimeScope();

			return ResolveRootValue();
		}

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(_rootLifetimeScope != null)
				{
					_rootLifetimeScope.Dispose();
					_rootLifetimeScope = null;
				}
			}
		}

		private void BuildRootLifetimeScope()
		{
			var builder = new ContainerBuilder();

			builder.RegisterModule(_rootModule);

			_rootLifetimeScope = builder.Build();
		}

		private T ResolveRootValue()
		{
			return _resolveValue(_rootLifetimeScope);
		}
	}
}