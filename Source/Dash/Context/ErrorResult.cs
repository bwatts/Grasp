using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using Grasp.Knowledge;

namespace Dash.Context
{
	public class ErrorResult : ContextResult
	{
		public static readonly Field<Exception> ExceptionField = Field.On<ErrorResult>.Backing(x => x.Exception);

		public ErrorResult(CancellationToken cancellationToken, DateTime whenStarted, DateTime whenStopped, Exception exception)
			: base(cancellationToken, whenStarted, whenStopped)
		{
			Contract.Requires(exception != null);

			Exception = exception;
		}

		public Exception Exception { get { return GetValue(ExceptionField); } private set { SetValue(ExceptionField, value); } }
	}
}