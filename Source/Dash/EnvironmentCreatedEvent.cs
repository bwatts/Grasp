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
	public class EnvironmentCreatedEvent : EnvironmentEvent
	{
		public EnvironmentCreatedEvent(EntityId workItemId, EntityId environmentId) : base(workItemId, environmentId)
		{}
	}
}