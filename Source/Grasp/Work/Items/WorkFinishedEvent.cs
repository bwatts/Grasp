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
	public class WorkFinishedEvent : Event
	{
		public static readonly Field<FullName> TopicNameField = Field.On<WorkFinishedEvent>.For(x => x.TopicName);
		public static readonly Field<TimeSpan> DurationField = Field.On<WorkFinishedEvent>.For(x => x.Duration);

		public WorkFinishedEvent(FullName workItemName, FullName topicName, TimeSpan duration) : base(workItemName)
		{
			Contract.Requires(topicName != FullName.Anonymous);
			Contract.Requires(Check.That(duration).IsNotNegative());

			TopicName = topicName;
			Duration = duration;
		}

		public FullName TopicName { get { return GetValue(TopicNameField); } private set { SetValue(TopicNameField, value); } }
		public TimeSpan Duration { get { return GetValue(DurationField); } private set { SetValue(DurationField, value); } }
	}
}