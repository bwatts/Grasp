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
		public static readonly Field<Guid> IdField = Field.On<WorkItemResource>.For(x => x.Id);
		public static readonly Field<DateTime> WhenStartedField = Field.On<WorkItemResource>.For(x => x.WhenStarted);
		public static readonly Field<Progress> ProgressField = Field.On<WorkItemResource>.For(x => x.Progress);
		public static readonly Field<TimeSpan?> RetryIntervalField = Field.On<WorkItemResource>.For(x => x.RetryInterval);
		public static readonly Field<Hyperlink> ResultLinkField = Field.On<WorkItemResource>.For(x => x.ResultLink);

		public WorkItemResource(MHeader header, Guid id, DateTime whenStarted, Progress progress, TimeSpan retryInterval) : base(header)
		{
			Contract.Requires(id != Guid.Empty);
			Contract.Requires(retryInterval >= TimeSpan.Zero);

			Id = id;
			WhenStarted = whenStarted;
			Progress = progress;
			RetryInterval = retryInterval;
		}

		public WorkItemResource(MHeader header, Guid id, DateTime whenStarted, Hyperlink resultLink) : base(header)
		{
			Contract.Requires(id != Guid.Empty);
			Contract.Requires(resultLink != null);

			Id = id;
			WhenStarted = whenStarted;
			ResultLink = resultLink;
		}

		public Guid Id { get { return GetValue(IdField); } private set { SetValue(IdField, value); } }
		public DateTime WhenStarted { get { return GetValue(WhenStartedField); } private set { SetValue(WhenStartedField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public TimeSpan? RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
		public Hyperlink ResultLink { get { return GetValue(ResultLinkField); } private set { SetValue(ResultLinkField, value); } }
	}
}