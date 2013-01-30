using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkProgressedEvent : WorkItemEvent
	{
		public static readonly Field<Progress> OldProgressField = Field.On<WorkProgressedEvent>.For(x => x.OldProgress);
		public static readonly Field<Progress> NewProgressField = Field.On<WorkProgressedEvent>.For(x => x.NewProgress);

		public WorkProgressedEvent(FullName workItemName, Progress oldProgress, Progress newProgress) : base(workItemName)
		{
			Contract.Requires(oldProgress < newProgress);
			Contract.Requires(newProgress < Progress.Complete);

			OldProgress = oldProgress;
			NewProgress = newProgress;
		}

		public Progress OldProgress { get { return GetValue(OldProgressField); } private set { SetValue(OldProgressField, value); } }
		public Progress NewProgress { get { return GetValue(NewProgressField); } private set { SetValue(NewProgressField, value); } }
	}
}