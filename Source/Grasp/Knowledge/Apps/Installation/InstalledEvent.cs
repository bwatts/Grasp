using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps.Installation
{
	public class InstalledEvent : InstallationEvent
	{
		public InstalledEvent(FullName workItemName, FullName environmentName, FullName appName) : base(workItemName, environmentName, appName)
		{}
	}
}