﻿using System;
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
		public static readonly Field<Uri> ResultLocationField = Field.On<WorkItem>.For(x => x.ResultLocation);

		public WorkItem(Guid id, string description, TimeSpan retryInterval)
		{
			Announce(new WorkItemCreatedEvent(id, description, retryInterval));
		}

		public string Description { get { return GetValue(DescriptionField); } private set { SetValue(DescriptionField, value); } }
		public TimeSpan RetryInterval { get { return GetValue(RetryIntervalField); } private set { SetValue(RetryIntervalField, value); } }
		public Progress Progress { get { return GetValue(ProgressField); } private set { SetValue(ProgressField, value); } }
		public Uri ResultLocation { get { return GetValue(ResultLocationField); } private set { SetValue(ResultLocationField, value); } }

		public void Handle(ReportProgressCommand command)
		{
			Contract.Requires(command != null);
			Contract.Requires(command.Progress >= Progress);

			if(Progress == Progress.Accepted && command.Progress > Progress.Accepted)
			{
				Announce(new ProgressStartedEvent(command.WorkItemId));
			}

			Announce(new ProgressReportedEvent(command.WorkItemId, Progress, command.Progress));

			if(command.Progress == Progress.Complete)
			{
				Announce(new WorkItemCompletedEvent(command.WorkItemId, command.ResultLocation));
			}
		}

		private void Observe(WorkItemCreatedEvent e)
		{
			SetValue(PersistentId.ValueField, e.WorkItemId);

			Description = e.Description;
			RetryInterval = e.RetryInterval;

			Progress = Progress.Accepted;

			SetWhenCreated(e.When);
			SetWhenModified(e.When);
		}

		private void Observe(ProgressReportedEvent e)
		{
			Progress = e.NewProgress;

			SetWhenModified(e.When);
		}

		private void Observe(WorkItemCompletedEvent e)
		{
			ResultLocation = e.ResultLocation;

			SetWhenModified(e.When);
		}
	}
}