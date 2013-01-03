using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasp.Messaging
{
	/// <summary>
	/// A message representing an occurrence on a timeline
	/// </summary>
	public abstract class Event : Message
	{
		public static readonly Field<EntityId> WorkItemIdField = Field.On<Event>.For(x => x.WorkItemId);
		public static readonly Field<DateTime> WhenField = Field.On<Event>.For(x => x.When);

		/// <summary>
		/// Initializes an event with the specified work item identifier and date/time
		/// </summary>
		/// <param name="workItemId">The identifier of the work item associated with this event</param>
		/// <param name="when">The date and time at which the event occurred (defaults to <see cref="DateTime.UtcNow"/>)</param>
		protected Event(EntityId workItemId = default(EntityId), DateTime? when = null)
		{
			WorkItemId = workItemId;
			When = when ?? DateTime.UtcNow;
		}

		/// <summary>
		/// Gets the identifier of the work item associated with this event (if any)
		/// </summary>
		public EntityId WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }

		/// <summary>
		/// Gets the date and time at which the event occurred
		/// </summary>
		public DateTime When { get { return GetValue(WhenField); } private set { SetValue(WhenField, value); } }
	}
}