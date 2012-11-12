using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work;
using Grasp.Work.Items;

namespace Grasp.Hypermedia
{
	public class WorkItemResource : HttpResource
	{
		public static readonly Field<Guid> IdField = Field.On<WorkItemResource>.For(x => x.Id);
		public static readonly Field<string> StatusField = Field.On<WorkItemResource>.For(x => x.Status);
		public static readonly Field<TimeSpan?> RetryIntervalField = Field.On<WorkItemResource>.For(x => x.RetryInterval);
		public static readonly Field<Progress?> ProgressField = Field.On<WorkItemResource>.For(x => x.Progress);
		public static readonly Field<Hyperlink> ResultLinkField = Field.On<WorkItemResource>.For(x => x.ResultLink);

		public WorkItemResource(HttpResourceHeader header, Guid id, string status, TimeSpan retryInterval) : base(header)
		{
			Contract.Requires(id != Guid.Empty);
			Contract.Requires(status != null);
			Contract.Requires(retryInterval >= TimeSpan.Zero);

			Id = id;
			Status = status;
			RetryInterval = retryInterval;
		}

		public WorkItemResource(HttpResourceHeader header, Guid id, string status, TimeSpan retryInterval, Progress progress) : this(header, id, status, retryInterval)
		{
			Progress = progress;
		}

		public WorkItemResource(HttpResourceHeader header, Guid id, string status, Hyperlink resultLink) : base(header)
		{
			Contract.Requires(id != Guid.Empty);
			Contract.Requires(status != null);
			Contract.Requires(resultLink != null);

			ResultLink = resultLink;
		}

		public Guid Id { get { return GetValue(IdField); } private set { SetValue(IdField, value); } }
		public string Status { get { return GetValue(StatusField); } private set { SetValue(StatusField, value); } }
		public TimeSpan? RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
		public Progress? Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public Hyperlink ResultLink { get { return GetValue(ResultLinkField); } private set { SetValue(ResultLinkField, value); } }
	}
}