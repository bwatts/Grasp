using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;
using Grasp.Work;

namespace Grasp.Knowledge.Apps.Installation
{
	public class AppInstallation : Notion
	{
		public static readonly Field<FullName> AppNameField = Field.On<AppInstallation>.For(x => x.AppName);
		public static readonly Field<AppInstallationStatus> StatusField = Field.On<AppInstallation>.For(x => x.Status);

		public AppInstallation(FullName appName)
		{
			Contract.Requires(appName != null);

			AppName = appName;
			Status = AppInstallationStatus.Installing;
		}

		public FullName AppName { get { return GetValue(AppNameField); } private set { SetValue(AppNameField, value); } }
		public AppInstallationStatus Status { get { return GetValue(StatusField); } set { SetValue(StatusField, value); } }

		public void OnInstalled()
		{
			Status = AppInstallationStatus.Installed;
		}

		public void OnInstallationStopped()
		{
			Status = AppInstallationStatus.InstallationStopped;
		}

		public void OnUninstalling()
		{
			Status = AppInstallationStatus.Uninstalling;
		}

		public void OnUninstallationStopped()
		{
			Status = AppInstallationStatus.UninstallationStopped;
		}
	}
}