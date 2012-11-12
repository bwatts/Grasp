using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkItemCompletedEvent : Event
	{
		public static readonly Field<Guid> WorkItemIdField = Field.On<WorkItemCompletedEvent>.For(x => x.WorkItemId);
		public static readonly Field<Uri> ResultResourceField = Field.On<WorkItemCompletedEvent>.For(x => x.ResultResource);

		public WorkItemCompletedEvent(Guid workItemId, Uri resultResource)
		{
			WorkItemId = workItemId;
			ResultResource = resultResource;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Uri ResultResource { get { return GetValue(ResultResourceField); } private set { SetValue(ResultResourceField, value); } }
	}
}