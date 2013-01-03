using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Dash.Applications.Definition
{
	public class RemovePackageSourceCommand : EnvironmentCommand
	{
		public static readonly Field<PackageSourceLocation> LocationField = Field.On<RemovePackageSourceCommand>.For(x => x.Location);

		public RemovePackageSourceCommand(EntityId workItemId, EntityId environmentId, PackageSourceLocation location) : base(workItemId, environmentId)
		{
			Contract.Requires(location != null);

			Location = location;
		}

		public PackageSourceLocation Location { get { return GetValue(LocationField); } private set { SetValue(LocationField, value); } }
	}
}