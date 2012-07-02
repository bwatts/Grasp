using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Infrastructure.Hosting
{
	public sealed class ConsoleDashHost : IDashHost
	{
		public void Execute(IDashRuntime runtime)
		{
			using(var source = new CancellationTokenSource())
			using(var registration = source.Token.Register(source.Cancel))
			{
				Execute(runtime, source);

				registration.Dispose();
			}
		}

		private static void Execute(IDashRuntime runtime, CancellationTokenSource source)
		{
			var task = ExecuteAsync(runtime, source);

			Console.ReadKey();

			if(!source.IsCancellationRequested)
			{
				source.Cancel();
			}

			WriteResult(task.Result);
		}

		private static Task<RuntimeResult> ExecuteAsync(IDashRuntime runtime, CancellationTokenSource source)
		{
			var context = new DashContext();

			context.SetValue(DashContext.CancellationTokenField, source.Token);

			return runtime.RunAsync(context);
		}

		private static void WriteResult(RuntimeResult result)
		{
			if(result is ErrorResult)
			{
				WriteError((ErrorResult) result);
			}
			else if(result is CancelledResult)
			{
				WriteCancelled((CancelledResult) result);
			}
			else
			{
				WriteStopped(result);
			}
		}

		private static void WriteError(ErrorResult errorResult)
		{
			Console.WriteLine("Error: " + errorResult.Exception.ToString());
		}

		private static void WriteCancelled(CancelledResult cancelledResult)
		{
			Console.WriteLine("Cancelled");
		}

		private static void WriteStopped(RuntimeResult result)
		{
			Console.WriteLine("Stopped");
		}
	}
}