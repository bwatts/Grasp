using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps
{
	public class StartEnvironmentCommand : EnvironmentCommand
	{
		public StartEnvironmentCommand(FullName workItemName, FullName environmentName) : base(workItemName, environmentName)
		{}
	}
}