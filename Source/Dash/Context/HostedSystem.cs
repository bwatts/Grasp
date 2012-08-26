using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cloak.Time;
using Dash.Context;
using Grasp.Knowledge;

namespace Dash.Context
{
	public abstract class HostedSystem : IHostable
	{
		private ITimeContext _timeContext { get; set; }

		protected HostedSystem(ITimeContext timeContext)
		{
			Contract.Requires(timeContext != null);

			_timeContext = timeContext;
		}

		public Task<ContextResult> StartAsync(CancellationToken cancellationToken)
		{
			var whenStarted = _timeContext.Now;

			return cancellationToken.IsCancellationRequested
				? RunCancelledAsync(cancellationToken, whenStarted)
				: RunProcessAsync(cancellationToken, whenStarted);
		}

		private Task<ContextResult> RunCancelledAsync(CancellationToken cancellationToken, DateTime whenStarted)
		{
			return Task.Factory.StartNew(() => GetResult(cancellationToken, whenStarted));
		}

		private Task<ContextResult> RunProcessAsync(CancellationToken cancellationToken, DateTime whenStarted)
		{
			cancellationToken.Register(StopProcessing);

			return Task
				.Factory
				.StartNew(StartProcessing, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default)
				.ContinueWith(task => GetResult(cancellationToken, whenStarted), TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.OnlyOnCanceled)
				.ContinueWith(task => GetResult(cancellationToken, whenStarted, task.Exception), TaskContinuationOptions.OnlyOnFaulted);
		}

		protected abstract void StartProcessing();

		protected abstract void StopProcessing();

		private ContextResult GetResult(CancellationToken cancellationToken, DateTime whenStarted)
		{
			return new ContextResult(cancellationToken, whenStarted, _timeContext.Now);
		}

		private ContextResult GetResult(CancellationToken cancellationToken, DateTime whenStarted, Exception exception)
		{
			return new ErrorResult(cancellationToken, whenStarted, _timeContext.Now, exception);
		}
	}
}