using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class WorkItem : Topic
	{
		public static readonly Field<TimeSpan> _timeoutField = Field.On<WorkItem>.For(x => x._timeout);
		public static readonly Field<Progress> _progressField = Field.On<WorkItem>.For(x => x._progress);
		public static readonly Field<ManyInOrder<Issue>> _issuesField = Field.On<WorkItem>.For(x => x._issues);

		private TimeSpan _timeout { get { return GetValue(_timeoutField); } set { SetValue(_timeoutField, value); } }
		private Progress _progress { get { return GetValue(_progressField); } set { SetValue(_progressField, value); } }
		private ManyInOrder<Issue> _issues { get { return GetValue(_issuesField); } set { SetValue(_issuesField, value); } }

		public WorkItem(FullName name, string description, TimeSpan pollInterval, TimeSpan timeout)
		{
			Announce(new WorkStartedEvent(name, description, pollInterval, timeout));
		}

		// Commands

		private void Handle(PollWorkCommand c)
		{
			if(ValidateCommand(c))
			{
				var age = GetAge();

				Announce(new WorkPolledEvent(c.WorkItemName, _progress, _timeout - age));

				if(_progress < Progress.Complete && age >= _timeout)
				{
					Announce(new WorkTimedOutEvent(c.WorkItemName, _timeout, age));
				}
			}
		}

		private void Handle(ReportProgressCommand c)
		{
			if(ValidateCommand(c))
			{
				if(c.Progress < Progress.Complete)
				{
					Announce(new WorkProgressedEvent(c.WorkItemName, _progress, c.Progress));
				}
				else
				{
					Announce(new WorkFinishedEvent(c.WorkItemName, c.TopicName, GetAge()));
				}
			}
		}

		private void Handle(ReportIssueCommand c)
		{
			if(ValidateCommand(c))
			{
				Announce(new WorkHasIssueEvent(c.WorkItemName, c.Issue));
			}
		}

		// Events

		private void Observe(WorkStartedEvent e)
		{
			OnCreated(e.WorkItemName, e.When);

			_timeout = e.Timeout;
			_progress = Progress.Accepted;
			_issues = new ManyInOrder<Issue>();
		}

		private void Observe(WorkProgressedEvent e)
		{
			OnModified(e.When);

			_progress = e.NewProgress;
		}

		private void Observe(WorkFinishedEvent e)
		{
			OnModified(e.When);

			_progress = Progress.Complete;
		}

		private void Observe(WorkTimedOutEvent e)
		{
			OnModified(e.When);

			_progress = Progress.Complete;
		}

		private void Observe(WorkHasIssueEvent e)
		{
			OnModified(e.When);

			// TODO: What to do with issues? Report them with events that finish the work item?

			_issues.AsWriteable().Add(e.Issue);
		}

		// Validation

		private bool ValidateCommand(Command command)
		{
			if(command.WorkItemName != Name)
			{
				AnnounceCommandFailed(command, "DifferentWorkItem", Resources.CommandAppliesToDifferentWorkItem.FormatInvariant(command.WorkItemName, Name));

				return false;
			}

			return true;
		}
	}
}