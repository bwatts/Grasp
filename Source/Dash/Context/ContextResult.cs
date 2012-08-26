using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using Grasp.Knowledge;

namespace Dash.Context
{
	public class ContextResult : Notion
	{
		public static readonly Field<CancellationToken> CancellationTokenField = Field.On<ContextResult>.Backing(x => x.CancellationToken);
		public static readonly Field<DateTime> WhenStartedField = Field.On<ContextResult>.Backing(x => x.WhenStarted);
		public static readonly Field<DateTime> WhenStoppedField = Field.On<ContextResult>.Backing(x => x.WhenStopped);

		public ContextResult(CancellationToken cancellationToken, DateTime whenStarted, DateTime whenStopped)
		{
			Contract.Requires(whenStarted <= whenStopped);

			CancellationToken = cancellationToken;
			WhenStarted = whenStarted;
			WhenStopped = whenStopped;
		}

		public CancellationToken CancellationToken { get { return GetValue(CancellationTokenField); } private set { SetValue(CancellationTokenField, value); } }
		public DateTime WhenStarted { get { return GetValue(WhenStartedField); } private set { SetValue(WhenStartedField, value); } }
		public DateTime WhenStopped { get { return GetValue(WhenStoppedField); } private set { SetValue(WhenStoppedField, value); } }

		public bool Cancelled
		{
			get { return CancellationToken.IsCancellationRequested; }
		}
	}
}