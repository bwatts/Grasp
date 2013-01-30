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
	public class Event : Message
	{
		public static readonly Field<DateTime> WhenField = Field.On<Event>.For(x => x.When);

		// TODO: Unoccurred? Really?

		/// <summary>
		/// The <see cref="DateTime"/> representing an event which has not occurred
		/// </summary>
		public static readonly DateTime UnoccurredDateTime = DateTime.MinValue;

		/// <summary>
		/// An event which has not occurred
		/// </summary>
		public static readonly Event Unoccurred = new Event(UnoccurredDateTime);

		/// <summary>
		/// Initializes an event with the specified date and time
		/// </summary>
		/// <param name="when">The date and time at which the event occurred (defaults to the time context specified by <see cref="Lifetime.TimeContextTrait"/>)</param>
		protected Event(DateTime? when = null)
		{
			When = when ?? this.Now();
		}

		/// <summary>
		/// Gets the date and time at which this event occurred
		/// </summary>
		public DateTime When { get { return GetValue(WhenField); } private set { SetValue(WhenField, value); } }

		/// <summary>
		/// Gets whether this event has occurred, defined as <see cref="When"/> not being equal to <see cref="UnoccurredDateTime"/>
		/// </summary>
		public bool HasOccurred
		{
			get { return When != UnoccurredDateTime; }
		}
	}
}