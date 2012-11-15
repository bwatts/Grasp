using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class ReportProgressCommand : Command
	{
		public static readonly Field<Guid> WorkItemIdField = Field.On<ReportProgressCommand>.For(x => x.WorkItemId);
		public static readonly Field<Progress> ProgressField = Field.On<ReportProgressCommand>.For(x => x.Progress);
		public static readonly Field<Uri> ResultLocationField = Field.On<ReportProgressCommand>.For(x => x.ResultLocation);

		public ReportProgressCommand(Guid workItemId, Progress progress)
		{
			Contract.Requires(workItemId != Guid.Empty);
			Contract.Requires(progress > Progress.Accepted && progress < Progress.Complete);

			WorkItemId = workItemId;
			Progress = progress;
		}

		public ReportProgressCommand(Guid workItemId, Uri resultLocation)
		{
			Contract.Requires(workItemId != Guid.Empty);
			Contract.Requires(resultLocation != null);

			WorkItemId = workItemId;
			ResultLocation = resultLocation;

			Progress = Progress.Complete;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public Uri ResultLocation { get { return GetValue(ResultLocationField); } private set { SetValue(ResultLocationField, value); } }
	}
}