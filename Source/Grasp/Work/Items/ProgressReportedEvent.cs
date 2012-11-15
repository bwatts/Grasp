using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class ProgressReportedEvent : Event
	{
		public static readonly Field<Guid> WorkItemIdField = Field.On<ProgressReportedEvent>.For(x => x.WorkItemId);
		public static readonly Field<Progress> OldProgressField = Field.On<ProgressReportedEvent>.For(x => x.OldProgress);
		public static readonly Field<Progress> NewProgressField = Field.On<ProgressReportedEvent>.For(x => x.NewProgress);

		public ProgressReportedEvent(Guid workItemId, Progress oldProgress, Progress newProgress)
		{
			WorkItemId = workItemId;
			OldProgress = oldProgress;
			NewProgress = newProgress;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Progress OldProgress { get { return GetValue(OldProgressField); } private set { SetValue(OldProgressField, value); } }
		public Progress NewProgress { get { return GetValue(NewProgressField); } private set { SetValue(NewProgressField, value); } }
	}
}