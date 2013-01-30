using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work
{
	/// <summary>
	/// A message representing an occurrence on a work item timeline
	/// </summary>
	public abstract class WorkItemEvent : Event
	{
		public static readonly Field<FullName> WorkItemNameField = Field.On<WorkItemEvent>.For(x => x.WorkItemName);

		/// <summary>
		/// Initializes an event with the specified work item identifier and date/time
		/// </summary>
		/// <param name="workItemName">The full name of the work item associated with this event</param>
		/// <param name="when">The date and time at which the event occurred (defaults to the time context specified by <see cref="Lifetime.TimeContextTrait"/>)</param>
		protected WorkItemEvent(FullName workItemName, DateTime? when = null) : base(when)
		{
			Contract.Requires(workItemName != null);

			WorkItemName = workItemName;
		}

		/// <summary>
		/// Gets the full name of the work item associated with this event
		/// </summary>
		public FullName WorkItemName { get { return GetValue(WorkItemNameField); } private set { SetValue(WorkItemNameField, value); } }
	}
}