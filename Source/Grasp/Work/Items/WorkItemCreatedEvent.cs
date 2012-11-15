using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkItemCreatedEvent : Event
	{
		public static readonly Field<Guid> WorkItemIdField = Field.On<WorkItemCreatedEvent>.For(x => x.WorkItemId);
		public static readonly Field<string> DescriptionField = Field.On<WorkItemCreatedEvent>.For(x => x.Description);
		public static readonly Field<TimeSpan> RetryIntervalField = Field.On<WorkItemCreatedEvent>.For(x => x.RetryInterval);

		public WorkItemCreatedEvent(Guid workItemId, string description, TimeSpan retryInterval)
		{
			WorkItemId = workItemId;
			Description = description;
			RetryInterval = retryInterval;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public TimeSpan RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
	}
}