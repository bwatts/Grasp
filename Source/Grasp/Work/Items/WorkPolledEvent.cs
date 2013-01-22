using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkPolledEvent : Event
	{
		public static readonly Field<Progress> ProgressField = Field.On<WorkPolledEvent>.For(x => x.Progress);
		public static readonly Field<TimeSpan> TimeBeforeTimeoutField = Field.On<WorkPolledEvent>.For(x => x.TimeBeforeTimeout);

		public WorkPolledEvent(FullName workItemName, Progress progress, TimeSpan timeBeforeTimeout) : base(workItemName)
		{
			Progress = progress;
			TimeBeforeTimeout = timeBeforeTimeout;
		}

		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public TimeSpan TimeBeforeTimeout { get { return GetValue(TimeBeforeTimeoutField); } private set { SetValue(TimeBeforeTimeoutField, value); } }
	}
}