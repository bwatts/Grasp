using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class ProgressStartedEvent : Event
	{
		public static readonly Field<Guid> WorkItemIdField = Field.On<ProgressStartedEvent>.For(x => x.WorkItemId);

		public ProgressStartedEvent(Guid workItemId)
		{
			WorkItemId = workItemId;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
	}
}