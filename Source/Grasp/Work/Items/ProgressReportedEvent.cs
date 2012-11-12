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
		public static readonly Field<Progress> ProgressField = Field.On<ProgressReportedEvent>.For(x => x.Progress);

		public ProgressReportedEvent(Guid workItemId, Progress progress)
		{
			WorkItemId = workItemId;
			Progress = progress;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
	}
}