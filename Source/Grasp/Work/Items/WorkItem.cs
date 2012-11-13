using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work.Persistence;

namespace Grasp.Work.Items
{
	public class WorkItem : Aggregate
	{
		public static readonly Field<string> DescriptionField = Field.On<WorkItem>.For(x => x.Description);
		public static readonly Field<TimeSpan> RetryIntervalField = Field.On<WorkItem>.For(x => x.RetryInterval);
		public static readonly Field<Progress> ProgressField = Field.On<WorkItem>.For(x => x.Progress);
		public static readonly Field<Uri> ResultResourceField = Field.On<WorkItem>.For(x => x.ResultResource);

		public WorkItem(Guid id, string description, TimeSpan retryInterval)
		{
			Contract.Requires(description != null);
			Contract.Requires(retryInterval > TimeSpan.Zero);

			Announce(new WorkItemCreatedEvent(id, description, retryInterval));
		}

		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public TimeSpan RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public Uri ResultResource { get { return GetValue(ResultResourceField); } private set { SetValue(ResultResourceField, value); } }

		public void ReportProgress(Progress progress, Uri resultUri = null)
		{
			if(Progress == Progress.Complete)
			{
				return;
			}

			if(Progress == Progress.Accepted && progress > Progress.Accepted)
			{
				Announce(new ProgressStartedEvent(Id));
			}

			Announce(new ProgressReportedEvent(Id, progress));

			if(progress == Progress.Complete)
			{
				Announce(new WorkItemCompletedEvent(Id, resultUri));
			}
		}

		private void Handle(WorkItemCreatedEvent e)
		{
			SetValue(PersistentId.ValueField, e.WorkItemId);

			Description = e.Description;
			RetryInterval = e.RetryInterval;

			Progress = Progress.Accepted;

			SetWhenCreated(e.When);
			SetWhenModified(e.When);
		}

		private void Handle(ProgressReportedEvent e)
		{
			Progress = e.Progress;

			SetWhenModified(e.When);
		}

		private void Handle(WorkItemCompletedEvent e)
		{
			ResultResource = e.ResultResource;

			SetWhenModified(e.When);
		}
	}
}