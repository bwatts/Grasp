using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dash.Infrastructure.Hosting
{
	public abstract class DashProcess : IDashProcess
	{
		public virtual Task StartAsync()
		{
			return Task.Factory.StartNew(StartProcessing);
		}

		public virtual Task StartAsync(CancellationToken cancellationToken)
		{
			cancellationToken.Register(StopProcessing);

			return Task.Factory.StartNew(StartProcessing, cancellationToken);
		}

		public virtual Task StartAsync(CancellationToken cancellationToken, TaskCreationOptions options)
		{
			cancellationToken.Register(StopProcessing);

			return Task.Factory.StartNew(StartProcessing, cancellationToken, options);
		}

		protected abstract void StartProcessing();

		protected abstract void StopProcessing();
	}
}