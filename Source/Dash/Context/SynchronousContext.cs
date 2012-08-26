using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Context
{
	public abstract class SynchronousContext : IDashContext
	{
		public virtual void HostSystem(IHostable system)
		{
			using(var source = new CancellationTokenSource())
			using(var registration = source.Token.Register(source.Cancel))
			{
				Execute(system, source);
			}
		}

		private void Execute(IHostable system, CancellationTokenSource source)
		{
			var task = system.StartAsync(source.Token);

			Execute();

			if(!source.IsCancellationRequested)
			{
				source.Cancel();
			}

			HandleResult(task.Result);
		}

		protected abstract void Execute();

		protected virtual void HandleResult(ContextResult result)
		{
			if(result is ErrorResult)
			{
				HandleError((ErrorResult) result);
			}
			else if(result.Cancelled)
			{
				HandleCancelled(result);
			}
			else
			{
				HandleFinished(result);
			}
		}

		protected virtual void HandleError(ErrorResult errorResult)
		{
			Contract.Requires(errorResult != null);

			throw errorResult.Exception;
		}

		protected virtual void HandleCancelled(ContextResult cancelledResult)
		{
			Contract.Requires(cancelledResult != null);
		}

		protected virtual void HandleFinished(ContextResult result)
		{
			Contract.Requires(result != null);
		}
	}
}