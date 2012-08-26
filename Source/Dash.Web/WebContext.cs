using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using Dash.Context;

namespace Dash.Web
{
	public sealed class WebContext : AsynchronousContext
	{
		private ICompositionRoot<object> _compositionRoot { get; set; }

		public WebContext(ICompositionRoot<object> compositionRoot)
		{
			Contract.Requires(compositionRoot != null);

			_compositionRoot = compositionRoot;
		}

		protected override void OnStarting()
		{
			// Observe only
			_compositionRoot.Value;
		}

		protected override void HandleError(ErrorResult errorResult)
		{
			// TODO: Log
		}

		protected override void HandleCancelled(ContextResult cancelledResult)
		{
			// TODO: Log
		}

		protected override void HandleFinished(ContextResult result)
		{
			// TODO: Log
		}
	}
}