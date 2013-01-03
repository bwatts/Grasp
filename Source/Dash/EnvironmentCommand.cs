using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Dash
{
	public class EnvironmentCommand : Command
	{
		public static readonly Field<EntityId> EnvironmentIdField = Field.On<EnvironmentCommand>.For(x => x.EnvironmentId);

		public EnvironmentCommand(EntityId workItemId, EntityId environmentId) : base(workItemId)
		{
			Contract.Requires(environmentId != EntityId.Unassigned);

			EnvironmentId = environmentId;
		}

		public EntityId EnvironmentId { get { return GetValue(EnvironmentIdField); } private set { SetValue(EnvironmentIdField, value); } }
	}
}