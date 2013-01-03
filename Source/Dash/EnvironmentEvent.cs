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
	public abstract class EnvironmentEvent : Event
	{
		public static readonly Field<EntityId> EnvironmentIdField = Field.On<EnvironmentEvent>.For(x => x.EnvironmentId);

		protected EnvironmentEvent(EntityId workItemId, EntityId environmentId) : base(workItemId)
		{
			Contract.Requires(environmentId != EntityId.Unassigned);

			EnvironmentId = environmentId;
		}

		public EntityId EnvironmentId { get { return GetValue(EnvironmentIdField); } private set { SetValue(EnvironmentIdField, value); } }
	}
}