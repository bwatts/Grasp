using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Items
{
	public class ReportIssueCommand : Command
	{
		public static readonly Field<Issue> IssueField = Field.On<ReportIssueCommand>.For(x => x.Issue);

		public ReportIssueCommand(FullName workItemName, Issue issue) : base(workItemName)
		{
			Contract.Requires(issue != null);

			Issue = issue;
		}

		public Issue Issue { get { return GetValue(IssueField); } private set { SetValue(IssueField, value); } }
	}
}