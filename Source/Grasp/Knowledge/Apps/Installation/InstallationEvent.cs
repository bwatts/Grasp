using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Knowledge.Apps.Installation
{
	public abstract class InstallationEvent : EnvironmentEvent
	{
		public static readonly Field<FullName> AppNameField = Field.On<InstallationEvent>.For(x => x.AppName);

		protected InstallationEvent(FullName workItemName, FullName environmentName, FullName appName) : base(workItemName, environmentName)
		{
			Contract.Requires(appName != null);

			AppName = appName;
		}

		public FullName AppName { get { return GetValue(AppNameField); } private set { SetValue(AppNameField, value); } }
	}
}