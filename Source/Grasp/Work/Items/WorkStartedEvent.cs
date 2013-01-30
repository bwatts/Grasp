using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Checks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkStartedEvent : WorkItemEvent
	{
		public static readonly Field<string> DescriptionField = Field.On<WorkStartedEvent>.For(x => x.Description);
		public static readonly Field<TimeSpan> PollIntervalField = Field.On<WorkStartedEvent>.For(x => x.PollInterval);
		public static readonly Field<TimeSpan> TimeoutField = Field.On<WorkStartedEvent>.For(x => x.Timeout);

		public WorkStartedEvent(FullName workItemName, string description, TimeSpan pollInterval, TimeSpan timeout) : base(workItemName)
		{
			Contract.Requires(description != null);
			Contract.Requires(Check.That(pollInterval).IsInfiniteOrPositive());
			Contract.Requires(Check.That(timeout).IsInfiniteOrPositive());

			Description = description;
			PollInterval = pollInterval;
			Timeout = timeout;
		}

		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public TimeSpan PollInterval { get { return GetValue(PollIntervalField); } private set { SetValue(PollIntervalField, value); } }
		public TimeSpan Timeout { get { return GetValue(TimeoutField); } private set { SetValue(TimeoutField, value); } }
	}
}