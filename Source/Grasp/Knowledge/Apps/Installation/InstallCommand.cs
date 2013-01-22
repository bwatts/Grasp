using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps.Installation
{
	public class InstallCommand : InstallationCommand
	{
		public InstallCommand(FullName workItemName, FullName environmentName, FullName applicationName) : base(workItemName, environmentName, applicationName)
		{}
	}
}