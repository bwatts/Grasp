using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps.Installation
{
	public class UninstallingEvent : InstallationEvent
	{
		public UninstallingEvent(FullName workItemName, FullName environmentName, FullName appName) : base(workItemName, environmentName, appName)
		{}
	}
}