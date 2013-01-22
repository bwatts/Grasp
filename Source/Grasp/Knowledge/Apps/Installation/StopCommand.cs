using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Work;

namespace Grasp.Knowledge.Apps.Installation
{
	public class StopCommand : InstallationCommand
	{
		public static readonly Field<Issue> IssueField = Field.On<StopCommand>.For(x => x.Issue);

		public StopCommand(FullName workItemName, FullName environmentName, FullName applicationName, Issue issue) : base(workItemName, environmentName, applicationName)
		{
			Contract.Requires(issue != null);

			Issue = issue;
		}

		public Issue Issue { get { return GetValue(IssueField); } private set { SetValue(IssueField, value); } }
	}
}