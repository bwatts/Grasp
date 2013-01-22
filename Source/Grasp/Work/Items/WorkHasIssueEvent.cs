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
	public class WorkHasIssueEvent : Event
	{
		public static readonly Field<Issue> IssueField = Field.On<WorkHasIssueEvent>.For(x => x.Issue);

		public WorkHasIssueEvent(FullName workItemName, Issue issue) : base(workItemName)
		{
			Issue = issue;
		}

		public Issue Issue { get { return GetValue(IssueField); } private set { SetValue(IssueField, value); } }
	}
}