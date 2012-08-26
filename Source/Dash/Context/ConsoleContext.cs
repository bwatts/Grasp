using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Context
{
	public class ConsoleContext : SynchronousContext
	{
		private ICompositionRoot<object> _compositionRoot { get; set; }

		public ConsoleContext(ICompositionRoot<object> compositionRoot)
		{
			Contract.Requires(compositionRoot != null);

			_compositionRoot = compositionRoot;
		}

		protected override void Execute()
		{
			// Observe only
			var value = _compositionRoot.Value;

			Console.ReadKey();
		}

		protected override void HandleError(ErrorResult errorResult)
		{
			Console.WriteLine("Error: " + errorResult.Exception.ToString());
		}

		protected override void HandleCancelled(ContextResult cancelledResult)
		{
			Console.WriteLine("Cancelled");
		}

		protected override void HandleFinished(ContextResult result)
		{
			Console.WriteLine("Finished");
		}
	}
}