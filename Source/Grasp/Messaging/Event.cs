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
		public static readonly Field<DateTime> WhenField = Field.On<Event>.For(x => x.When);

		protected Event(DateTime? when = null, Guid? id = null) : base(id)
		{
			When = when ?? DateTime.UtcNow;
		}

		public DateTime When { get { return GetValue(WhenField); } private set { SetValue(WhenField, value); } }
	}
}