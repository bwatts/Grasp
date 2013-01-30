using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Git
{
	public class EventFileBody : Notion, IEnumerable<Event>
	{
		public static readonly Field<IEnumerable<Event>> _eventsField = Field.On<EventFileBody>.For(x => x._events);
		public static readonly Field<bool> AppendingField = Field.On<EventFileBody>.For(x => x.Appending);

		private IEnumerable<Event> _events { get { return GetValue(_eventsField); } set { SetValue(_eventsField, value); } }

		public EventFileBody(IEnumerable<Event> events, bool appending = false)
		{
			Contract.Requires(events != null);

			_events = events;
			Appending = appending;
		}

		public bool Appending { get { return GetValue(AppendingField); } private set { SetValue(AppendingField, value); } }

		public IEnumerator<Event> GetEnumerator()
		{
			return _events.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}