using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dash.Applications.Installation
{
	public enum InstallationStatus
	{
		InProgress,
		Complete,
		Stopped,
		Uninstalling,
		UninstallationStopped
	}
}