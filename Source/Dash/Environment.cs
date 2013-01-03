using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dash.Applications;
using Dash.Applications.Definition;
using Dash.Applications.Installation;
using Grasp;
using Grasp.Knowledge;
using Grasp.Messaging;
using Grasp.Work;
using Grasp.Work.Persistence;

namespace Dash
{
	/// <summary>
	/// A context in which applications participate in the Dash ecosystem
	/// </summary>
	public class Environment : Aggregate
	{
		public static readonly Field<ManyInOrder<PackageSource>> _packageSourcesField = Field.On<Environment>.For(x => x._packageSources);
		public static readonly Field<Many<Application>> _applicationsField = Field.On<Environment>.For(x => x._applications);

		private ManyInOrder<PackageSource> _packageSources { get { return GetValue(_packageSourcesField); } set { SetValue(_packageSourcesField, value); } }
		private Many<Application> _applications { get { return GetValue(_applicationsField); } set { SetValue(_applicationsField, value); } }

		public Environment(EntityId workItemId, EntityId id)
		{
			Announce(new EnvironmentCreatedEvent(workItemId, id));
		}

		private void Handle(AddPackageSourceCommand command)
		{
			// TODO: Validate command

			Announce(new PackageSourceAddedEvent(command.WorkItemId, command.EnvironmentId, command.Location));
		}

		private void Handle(RemovePackageSourceCommand command)
		{
			// TODO: Validate command

			Announce(new PackageSourceRemovedEvent(command.WorkItemId, command.EnvironmentId, command.Location));
		}

		private void Handle(InstallCommand command)
		{
			// TODO: Validate command

			Announce(new InstallingEvent(command.WorkItemId, command.EnvironmentId, command.ApplicationName, GetPackageSourceLocations()));
		}

		private void Observe(EnvironmentCreatedEvent e)
		{
			_applications = new Many<Application>();

			OnCreated(e.EnvironmentId, e.When);
		}

		private void Observe(PackageSourceAddedEvent e)
		{
			_packageSources.AsWriteable().Add(new PackageSource(e.Location));

			OnModified(e.When);
		}

		private void Observe(PackageSourceRemovedEvent e)
		{
			_packageSources.AsWriteable().Remove(_packageSources.First(packageSource => packageSource.Location == e.Location));

			OnModified(e.When);
		}

		private void Observe(InstallingEvent e)
		{
			_applications.AsWriteable().Add(new Application(e.ApplicationName));

			OnModified(e.When);
		}

		private IEnumerable<PackageSourceLocation> GetPackageSourceLocations()
		{
			return _packageSources.Select(packageSource => packageSource.Location);
		}
	}
}