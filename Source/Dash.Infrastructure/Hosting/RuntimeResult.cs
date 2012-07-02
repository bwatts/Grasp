using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Grasp.Knowledge;

namespace Dash.Infrastructure.Hosting
{
	public class RuntimeResult : Notion
	{
		public static readonly Field<CancellationToken> CancellationTokenField = Field.On<RuntimeResult>.Backing(x => x.CancellationToken);
		public static readonly Field<DateTime> WhenStartedField = Field.On<RuntimeResult>.Backing(x => x.WhenStarted);
		public static readonly Field<DateTime> WhenStoppedField = Field.On<RuntimeResult>.Backing(x => x.WhenStopped);

		public CancellationToken CancellationToken { get { return GetValue(CancellationTokenField); } }
		public DateTime WhenStarted { get { return GetValue(WhenStartedField); } }
		public DateTime WhenStopped { get { return GetValue(WhenStoppedField); } }
	}
}