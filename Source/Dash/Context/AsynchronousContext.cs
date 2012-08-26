using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Context
{
	[ContractClass(typeof(AsynchronousContextContract))]
	public abstract class AsynchronousContext : IDashContext
	{
		private CancellationTokenSource _cancellationTokenSource { get; set; }
		private CancellationTokenRegistration _cancellationTokenRegistration { get; set; }
		private Task _task { get; set; }

		public virtual void HostSystem(IHostable system)
		{
			EnsureNotExecuting();

			RegisterCancellationToken();

			StartTask(system);
		}

		private void EnsureNotExecuting()
		{
			if(_task != null)
			{
				throw new InvalidOperationException("This context is already executing a system");
			}
		}

		private void RegisterCancellationToken()
		{
			_cancellationTokenSource = new CancellationTokenSource();

			_cancellationTokenRegistration = _cancellationTokenSource.Token.Register(_cancellationTokenSource.Cancel);
		}

		private void StartTask(IHostable system)
		{
			OnStarting();

			_task = system.StartAsync(_cancellationTokenSource.Token).ContinueWith(task => OnDone(task));
		}

		private void OnDone(Task<ContextResult> task)
		{
			if(!_cancellationTokenSource.IsCancellationRequested)
			{
				_cancellationTokenSource.Cancel();
			}

			HandleResult(task.Result);

			Clear();
		}

		private void Clear()
		{
			_cancellationTokenRegistration.Dispose();
			_cancellationTokenSource.Dispose();

			_cancellationTokenSource = null;
			_cancellationTokenRegistration = default(CancellationTokenRegistration);
			_task = null;
		}

		protected abstract void OnStarting();

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

		protected abstract void HandleCancelled(ContextResult cancelledResult);

		protected abstract void HandleFinished(ContextResult result);
	}

	[ContractClassFor(typeof(AsynchronousContext))]
	internal abstract class AsynchronousContextContract : AsynchronousContext
	{
		protected override void OnStarting()
		{}

		protected override void HandleCancelled(ContextResult cancelledResult)
		{
			Contract.Requires(cancelledResult != null);
		}

		protected override void HandleFinished(ContextResult result)
		{
			Contract.Requires(result != null);
		}
	}
}