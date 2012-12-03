using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work.Persistence;

namespace Grasp.Work.Items
{
	public class WorkItem : Aggregate, IActor<ReportProgressCommand>
	{
		public static readonly Field<string> DescriptionField = Field.On<WorkItem>.For(x => x.Description);
		public static readonly Field<Progress> ProgressField = Field.On<WorkItem>.For(x => x.Progress);
		public static readonly Field<TimeSpan> RetryIntervalField = Field.On<WorkItem>.For(x => x.RetryInterval);
		public static readonly Field<Uri> ResultLocationField = Field.On<WorkItem>.For(x => x.ResultLocation);

		public WorkItem(EntityId id, string description, TimeSpan retryInterval)
		{
			Announce(new WorkItemCreatedEvent(id, description, retryInterval));
		}

		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public TimeSpan RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
		public Uri ResultLocation { get { return GetValue(ResultLocationField); } private set { SetValue(ResultLocationField, value); } }

		public void Act(ReportProgressCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.WorkItemId == Id);
			Contract.Requires(command.Progress >= Progress);

			Announce(command.Progress == Progress.Complete
				? new ProgressReportedEvent(Id, Progress, command.ResultLocation)
				: new ProgressReportedEvent(Id, Progress, command.Progress));
		}

		private void Observe(WorkItemCreatedEvent e)
		{
			OnCreated(e.WorkItemId, e.When);

			Description = e.Description;
			RetryInterval = e.RetryInterval;

			Progress = Progress.Accepted;
		}

		private void Observe(ProgressReportedEvent e)
		{
			OnModified(e.When);

			Progress = e.NewProgress;
			ResultLocation = e.ResultLocation;
		}
	}
}