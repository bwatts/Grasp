using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dash.Applications.Installation;
using Grasp;
using Grasp.Messaging;

namespace Dash.Infrastructure
{
	public sealed class ApplicationInstaller : Notion, ISubscriber<InstallingEvent>
	{
		public static readonly Field<IInstallationService> _installationServiceField = Field.On<ApplicationInstaller>.For(x => x._installationService);

		private IInstallationService _installationService { get { return GetValue(_installationServiceField); } set { SetValue(_installationServiceField, value); } }

		public ApplicationInstaller(IInstallationService installationService)
		{
			Contract.Requires(installationService != null);

			_installationService = installationService;
		}

		public Task ObserveAsync(InstallingEvent e)
		{
			return _installationService.InstallApplicationAsync(e.WorkItemId, e.EnvironmentId, e.ApplicationName, e.PackageSourceLocations);
		}
	}
}