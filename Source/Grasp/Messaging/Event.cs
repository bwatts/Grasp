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
		public static readonly Field<FullName> WorkItemNameField = Field.On<Event>.For(x => x.WorkItemName);

		/// <summary>
		/// Initializes an event with the specified work item identifier and date/time
		/// </summary>
		/// <param name="workItemName">The full name of the work item associated with this event (defaults to <see cref="FullName.Anonymous"/>)</param>
		/// <param name="when">The date and time at which the event occurred (defaults to the time context specified by <see cref="Lifetime"/>)</param>
		protected Event(FullName workItemName = default(FullName), DateTime? when = null)
		{
			WorkItemName = workItemName ?? FullName.Anonymous;
			When = when ?? this.Now();
		}

		/// <summary>
		/// Gets the full name of the work item associated with this event (if any)
		/// </summary>
		public FullName WorkItemName { get { return GetValue(WorkItemNameField); } private set { SetValue(WorkItemNameField, value); } }

		/// <summary>
		/// Gets the date and time at which the event occurred
		/// </summary>
		public DateTime When { get { return GetValue(Lifetime.WhenCreatedField); } private set { SetValue(Lifetime.WhenCreatedField, value); } }
	}
}