using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkItemCompletedEvent : Event
	{
		public static readonly Field<Guid> WorkItemIdField = Field.On<WorkItemCompletedEvent>.For(x => x.WorkItemId);
		public static readonly Field<Uri> ResultLocationField = Field.On<WorkItemCompletedEvent>.For(x => x.ResultLocation);

		public WorkItemCompletedEvent(Guid workItemId, Uri resultLocation)
		{
			WorkItemId = workItemId;
			ResultLocation = resultLocation;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Uri ResultLocation { get { return GetValue(ResultLocationField); } private set { SetValue(ResultLocationField, value); } }
	}
}