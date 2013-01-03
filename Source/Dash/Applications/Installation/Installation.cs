using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dash.Applications.Definition;
using Grasp;
using Grasp.Work;

namespace Dash.Applications.Installation
{
	public class Installation : Aggregate
	{
		public static readonly Field<EntityId> EnvironmentIdField = Field.On<Installation>.For(x => x.EnvironmentId);
		public static readonly Field<FullName> ApplicationNameField = Field.On<Installation>.For(x => x.ApplicationName);
		public static readonly Field<InstallationStatus> StatusField = Field.On<Installation>.For(x => x.Status);

		public Installation(EntityId workItemId, EntityId environmentId, FullName applicationName, IEnumerable<PackageSourceLocation> packageSourceLocations)
		{
			Announce(new InstallingEvent(workItemId, environmentId, applicationName, packageSourceLocations));
		}

		public EntityId EnvironmentId { get { return GetValue(EnvironmentIdField); } private set { SetValue(EnvironmentIdField, value); } }
		public FullName ApplicationName { get { return GetValue(ApplicationNameField); } private set { SetValue(ApplicationNameField, value); } }
		public InstallationStatus Status { get { return GetValue(StatusField); } private set { SetValue(StatusField, value); } }

		private void Handle(CommitInstallationCommand c)
		{
			// TODO: Validate command

			Announce(new InstallationCommittedEvent(c.WorkItemId, c.EnvironmentId, c.ApplicationName));
		}

		private void Handle(StopInstallationCommand c)
		{
			// TODO: Validate command

			Announce(new InstallationStoppedEvent(c.WorkItemId, c.EnvironmentId, c.ApplicationName));
		}

		private void Handle(UninstallCommand c)
		{
			// TODO: Validate command

			Announce(new UninstallingEvent(c.WorkItemId, c.EnvironmentId, c.ApplicationName));
		}

		private void Handle(StopUninstallationCommand c)
		{
			// TODO: Validate command

			Announce(new UninstallationStoppedEvent(c.WorkItemId, c.EnvironmentId, c.ApplicationName));
		}

		private void Observe(InstallingEvent e)
		{
			EnvironmentId = e.EnvironmentId;
			ApplicationName = e.ApplicationName;

			Status = InstallationStatus.InProgress;
		}

		private void Observe(InstallationCommittedEvent e)
		{
			Status = InstallationStatus.Complete;
		}

		private void Observe(InstallationStoppedEvent e)
		{
			Status = InstallationStatus.Stopped;
		}

		private void Observe(UninstallingEvent e)
		{
			Status = InstallationStatus.Uninstalling;
		}

		private void Observe(UninstallationStoppedEvent e)
		{
			Status = InstallationStatus.UninstallationStopped;
		}
	}
}