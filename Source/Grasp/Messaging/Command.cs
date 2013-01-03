using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// A message with the intent to initiate an action
	/// </summary>
	public abstract class Command : Message
	{
		public static readonly Field<EntityId> WorkItemIdField = Field.On<Command>.For(x => x.WorkItemId);

		/// <summary>
		/// Initializes a command with the specified handle to a work item which tracks its progress
		/// </summary>
		/// <param name="workItemId">The identifier of the work item tracking the command's progress</param>
		protected Command(EntityId workItemId = default(EntityId))
		{
			WorkItemId = workItemId;
		}

		/// <summary>
		/// Gets the identifier of the work item tracking the command's progress
		/// </summary>
		public EntityId WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
	}
}