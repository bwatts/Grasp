using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Hypermedia.Linq;
using Grasp.Work;
using Grasp.Work.Items;

namespace Grasp.Hypermedia
{
	public class WorkItemResource : HttpResource
	{
		public static readonly Field<DateTime> WhenStartedField = Field.On<WorkItemResource>.For(x => x.WhenStarted);
		public static readonly Field<Progress> ProgressField = Field.On<WorkItemResource>.For(x => x.Progress);
		public static readonly Field<TimeSpan?> RetryIntervalField = Field.On<WorkItemResource>.For(x => x.RetryInterval);
		public static readonly Field<Hyperlink> ResultLinkField = Field.On<WorkItemResource>.For(x => x.ResultLink);

		public WorkItemResource(MHeader header, DateTime whenStarted, Progress progress, TimeSpan retryInterval) : base(header)
		{
			Contract.Requires(retryInterval >= TimeSpan.Zero);

			WhenStarted = whenStarted;
			Progress = progress;
			RetryInterval = retryInterval;
		}

		public WorkItemResource(MHeader header, DateTime whenStarted, Hyperlink resultLink) : base(header)
		{
			Contract.Requires(resultLink != null);

			WhenStarted = whenStarted;
			Progress = Progress.Complete;
			ResultLink = resultLink;
		}

		public DateTime WhenStarted { get { return GetValue(WhenStartedField); } private set { SetValue(WhenStartedField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public TimeSpan? RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
		public Hyperlink ResultLink { get { return GetValue(ResultLinkField); } private set { SetValue(ResultLinkField, value); } }
	}
}