using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Messaging;

namespace Grasp.Work
{
	/// <summary>
	/// A specific revision of an <see cref="Aggregate"/>, composed of the prior version and a set of events defining the current version
	/// </summary>
	public class AggregateVersion : Notion, IEnumerable<Event>
	{
		public static readonly Field<AggregateVersion> BaseVersionField = Field.On<AggregateVersion>.For(x => x.BaseVersion);
		public static readonly Field<int> NumberField = Field.On<AggregateVersion>.For(x => x.Number);
		public static readonly Field<IEnumerable<Event>> _eventsField = Field.On<AggregateVersion>.For(x => x._events);

		private IEnumerable<Event> _events { get { return GetValue(_eventsField); } set { SetValue(_eventsField, value); } }

		public AggregateVersion(int number, IEnumerable<Event> events)
		{
			Contract.Requires(number > 0);
			Contract.Requires(events != null);

			Number = number;
			_events = events;
		}

		public AggregateVersion(AggregateVersion baseVersion, int number, IEnumerable<Event> events) : this(number, events)
		{
			BaseVersion = baseVersion;
		}

		public AggregateVersion BaseVersion { get { return GetValue(BaseVersionField); } private set { SetValue(BaseVersionField, value); } }
		public int Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		
		public IEnumerator<Event> GetEnumerator()
		{
			var version = this;

			do
			{
				foreach(var @event in version._events)
				{
					yield return @event;
				}

				version = version.BaseVersion;
			}
			while(version != null);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}