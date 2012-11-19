using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class ProgressReportedEvent : Event
	{
		public static readonly Field<EntityId> WorkItemIdField = Field.On<ProgressReportedEvent>.For(x => x.WorkItemId);
		public static readonly Field<Progress> OldProgressField = Field.On<ProgressReportedEvent>.For(x => x.OldProgress);
		public static readonly Field<Progress> NewProgressField = Field.On<ProgressReportedEvent>.For(x => x.NewProgress);
		public static readonly Field<Uri> ResultLocationField = Field.On<ProgressReportedEvent>.For(x => x.ResultLocation);

		public ProgressReportedEvent(EntityId workItemId, Progress oldProgress, Progress newProgress)
		{
			Contract.Requires(workItemId != EntityId.Unassigned);
			Contract.Requires(oldProgress < newProgress);
			Contract.Requires(newProgress < Progress.Complete);

			WorkItemId = workItemId;
			OldProgress = oldProgress;
			NewProgress = newProgress;
		}

		public ProgressReportedEvent(EntityId workItemId, Progress oldProgress, Uri resultLocation)
		{
			Contract.Requires(workItemId != EntityId.Unassigned);
			Contract.Requires(oldProgress < Progress.Complete);
			Contract.Requires(resultLocation != null);

			WorkItemId = workItemId;
			OldProgress = oldProgress;
			NewProgress = Progress.Complete;
			ResultLocation = resultLocation;
		}

		public EntityId WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Progress OldProgress { get { return GetValue(OldProgressField); } private set { SetValue(OldProgressField, value); } }
		public Progress NewProgress { get { return GetValue(NewProgressField); } private set { SetValue(NewProgressField, value); } }
		public Uri ResultLocation { get { return GetValue(ResultLocationField); } private set { SetValue(ResultLocationField, value); } }
	}
}