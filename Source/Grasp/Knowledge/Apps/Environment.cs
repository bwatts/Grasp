using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Checks;
using Grasp.Knowledge.Apps.Installation;
using Grasp.Work;
using Grasp.Messaging;

namespace Grasp.Knowledge.Apps
{
	public class Environment : Topic
	{
		public static readonly Field<FullName> _environmentNameField = Field.On<Environment>.For(x => x._environmentName);
		public static readonly Field<ManyInOrder<AppSource>> _appSourcesField = Field.On<Environment>.For(x => x._appSources);
		public static readonly Field<Many<AppInstallation>> _appsField = Field.On<Environment>.For(x => x._apps);

		private FullName _environmentName { get { return GetValue(_environmentNameField); } set { SetValue(_environmentNameField, value); } }
		private ManyInOrder<AppSource> _appSources { get { return GetValue(_appSourcesField); } set { SetValue(_appSourcesField, value); } }
		private Many<AppInstallation> _apps { get { return GetValue(_appsField); } set { SetValue(_appsField, value); } }

		public Environment(FullName workItemName, FullName name, string title)
		{
			Announce(new EnvironmentStartedEvent(workItemName, name, title));
		}

		// App source commands

		private void Handle(AddAppSourceCommand c)
		{
			if(ValidateEnvironmentCommand(c) && !HasAppSource(c.AppSource))
			{
				Announce(new AppSourceAddedEvent(c.WorkItemName, c.EnvironmentName, c.AppSource));
			}
		}

		private void Handle(RemoveAppSourceCommand c)
		{
			if(ValidateEnvironmentCommand(c) && HasAppSource(c.AppSource))
			{
				Announce(new AppSourceRemovedEvent(c.WorkItemName, c.EnvironmentName, c.AppSource));
			}
		}

		// Installation commands

		private void Handle(InstallCommand c)
		{
			AppInstallation app;

			if(ValidateInstallationCommand(c, out app, Resources.CannotCommit, AppInstallationStatus.Installing, AppInstallationStatus.Uninstalling))
			{
				Announce(new InstallingEvent(c.WorkItemName, c.EnvironmentName, c.AppName));
			}
		}

		private void Handle(CommitCommand c)
		{
			AppInstallation app;

			if(ValidateInstallationCommand(c, out app, Resources.CannotCommit, AppInstallationStatus.Installing, AppInstallationStatus.Uninstalling))
			{
				if(app.Status == AppInstallationStatus.Installing)
				{
					Announce(new InstalledEvent(c.WorkItemName, c.EnvironmentName, c.AppName));
				}
				else
				{
					Announce(new UninstalledEvent(c.WorkItemName, c.EnvironmentName, c.AppName));
				}
			}
		}

		private void Handle(StopCommand c)
		{
			AppInstallation app;

			if(ValidateInstallationCommand(c, out app, Resources.CannotStop, AppInstallationStatus.Installing, AppInstallationStatus.Uninstalling))
			{
				if(app.Status == AppInstallationStatus.Installing)
				{
					Announce(new InstallationStoppedEvent(c.WorkItemName, c.EnvironmentName, c.AppName));
				}
				else
				{
					Announce(new UninstallationStoppedEvent(c.WorkItemName, c.EnvironmentName, c.AppName));
				}
			}
		}

		private void Handle(UninstallCommand c)
		{
			AppInstallation app;

			if(ValidateInstallationCommand(c, out app, Resources.CannotUninstall, AppInstallationStatus.Installed, AppInstallationStatus.InstallationStopped, AppInstallationStatus.UninstallationStopped))
			{
				Announce(new UninstallingEvent(c.WorkItemName, c.EnvironmentName, c.AppName));
			}
		}

		// Environment events

		private void Observe(EnvironmentStartedEvent e)
		{
			OnCreated(e.EnvironmentName, e.When);

			_environmentName = e.EnvironmentName;
			_appSources = new ManyInOrder<AppSource>();
			_apps = new Many<AppInstallation>();
		}

		private void Observe(AppSourceAddedEvent e)
		{
			OnModified(e.When);

			_appSources.AsWriteable().Add(e.AppSource);
		}

		private void Observe(AppSourceRemovedEvent e)
		{
			OnModified(e.When);

			_appSources.AsWriteable().Remove(e.AppSource);
		}

		// Installation events

		private void Observe(InstallingEvent e)
		{
			OnModified(e.When);

			_apps.AsWriteable().Add(new AppInstallation(e.AppName));
		}

		private void Observe(InstalledEvent e)
		{
			OnModified(e.When);

			GetApp(e.AppName).OnInstalled();
		}

		private void Observe(InstallationStoppedEvent e)
		{
			OnModified(e.When);

			GetApp(e.AppName).OnInstallationStopped();
		}

		private void Observe(UninstallingEvent e)
		{
			OnModified(e.When);

			GetApp(e.AppName).OnUninstalling();
		}

		private void Observe(UninstallationStoppedEvent e)
		{
			OnModified(e.When);

			GetApp(e.AppName).OnUninstallationStopped();
		}

		private void Observe(UninstalledEvent e)
		{
			OnModified(e.When);

			_apps.AsWriteable().Remove(GetApp(e.AppName));
		}

		// Validation

		private bool ValidateEnvironmentCommand(EnvironmentCommand command)
		{
			if(command.EnvironmentName != _environmentName)
			{
				AnnounceCommandFailed(command, "DifferentEnvironment", Resources.CommandAppliesToDifferentEnvironment.FormatInvariant(command.EnvironmentName, _environmentName));

				return false;
			}

			return true;
		}

		private bool ValidateInstallationCommand(InstallationCommand command, out AppInstallation app, string statusFormat, params AppInstallationStatus[] validStatuses)
		{
			ValidateEnvironmentCommand(command);

			app = GetApp(command.AppName);

			if(app == null)
			{
				AnnounceCommandFailed(command, "UnknownApp", Resources.CommandAppliesToUnknownApp.FormatInvariant(command.AppName));

				return false;
			}

			if(!Check.That(app.Status).IsIn(validStatuses))
			{
				AnnounceCommandFailed(command, "InvalidStatusTransition", statusFormat.FormatInvariant(app.Status));

				return false;
			}

			return true;
		}

		// Objects

		private bool HasAppSource(AppSource appSource)
		{
			return _appSources.Contains(appSource);
		}

		private AppInstallation GetApp(FullName name)
		{
			return _apps.FirstOrDefault(app => app.AppName == name);
		}
	}
}