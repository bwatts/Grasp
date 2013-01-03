using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dash.Applications.Definition;
using Grasp;
using Grasp.Messaging;

namespace Dash.Applications.Installation
{
	public class InstallingEvent : EnvironmentEvent
	{
		public static readonly Field<FullName> ApplicationNameField = Field.On<InstallingEvent>.For(x => x.ApplicationName);
		public static readonly Field<ManyInOrder<PackageSourceLocation>> PackageSourceLocationsField = Field.On<InstallingEvent>.For(x => x.PackageSourceLocations);

		public InstallingEvent(EntityId workItemId, EntityId environmentId, FullName applicationName, IEnumerable<PackageSourceLocation> packageSourceLocations) : base(workItemId, environmentId)
		{
			Contract.Requires(applicationName != null);
			Contract.Requires(packageSourceLocations != null);

			ApplicationName = applicationName;
			PackageSourceLocations = packageSourceLocations.ToManyInOrder();
		}

		public FullName ApplicationName { get { return GetValue(ApplicationNameField); } private set { SetValue(ApplicationNameField, value); } }
		public ManyInOrder<PackageSourceLocation> PackageSourceLocations { get { return GetValue(PackageSourceLocationsField); } private set { SetValue(PackageSourceLocationsField, value); } }
	}
}