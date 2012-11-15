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

		public ReportProgressCommand(Guid workItemId, Progress progress, Uri resultLocation = null)
		{
			Contract.Requires(workItemId != Guid.Empty);
			Contract.Requires(progress < Progress.Complete || resultLocation == null);

			WorkItemId = workItemId;
			Progress = progress;
			ResultLocation = resultLocation;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public Uri ResultLocation { get { return GetValue(ResultLocationField); } private set { SetValue(ResultLocationField, value); } }
	}
}