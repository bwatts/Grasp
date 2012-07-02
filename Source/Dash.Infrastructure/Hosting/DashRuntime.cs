using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cloak.Time;
using Grasp.Knowledge;

namespace Dash.Infrastructure.Hosting
{



	



	public sealed class TodoodleProcess : DashProcess
	{
		protected override void StartProcessing()
		{
			throw new NotImplementedException();
		}

		protected override void StopProcessing()
		{
			throw new NotImplementedException();
		}
	}





	public sealed class DashRuntime : IDashRuntime
	{
		private readonly ITimeContext _timeContext;
		private readonly IDashProcess _process;

		public DashRuntime(ITimeContext timeContext, IDashProcess process)
		{
			Contract.Requires(timeContext != null);
			Contract.Requires(process != null);

			_timeContext = timeContext;
			_process = process;
		}

		public Task<RuntimeResult> RunAsync(CancellationToken cancellationToken)
		{
			var whenStarted = _timeContext.Now;

			return context.WasCancelled
				? RunCancelledAsync(context, whenStarted)
				: RunProcessAsync(context, whenStarted);
		}

		private Task<RuntimeResult> RunCancelledAsync(CancellationToken cancellationToken, DateTime whenStarted)
		{
			return Task.Factory.StartNew(() =>
			{
				var result = new CancelledResult();

				SetBaseValues(result, cancellationToken, whenStarted);

				return result;
			});
		}

		private Task<RuntimeResult> RunProcessAsync(CancellationToken cancellationToken, DateTime whenStarted)
		{
			_process
				.StartAsync(context.CancellationToken, TaskCreationOptions.LongRunning)
				.ContinueWith<RuntimeResult>(task =>
				{
					var result = new ErrorResult();

					SetBaseValues(result, cancellationToken, whenStarted);

					result.SetValue(ErrorResult.ExceptionField, task.Exception);

					return result;
				},
				TaskContinuationOptions.OnlyOnFaulted)
				.Wait(cancellationToken);

			var result = new RuntimeResult();

			SetBaseValues(result, cancellationToken, whenStarted);

			return result;
		}

		private void SetBaseValues(RuntimeResult result, CancellationToken cancellationToken, DateTime whenStarted)
		{
			result.SetValue(RuntimeResult.CancellationTokenField, cancellationToken);
			result.SetValue(RuntimeResult.WhenStartedField, whenStarted);
			result.SetValue(RuntimeResult.WhenStoppedField, _timeContext.Now);
		}
	}
}