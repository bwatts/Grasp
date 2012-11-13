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
		public static readonly Field<Guid> RevisionIdField = Field.On<AggregateVersion>.For(x => x.RevisionId);
		public static readonly Field<IEnumerable<Event>> _eventsField = Field.On<AggregateVersion>.For(x => x._events);

		private IEnumerable<Event> _events { get { return GetValue(_eventsField); } set { SetValue(_eventsField, value); } }

		public AggregateVersion(Guid revisionId, IEnumerable<Event> events)
		{
			Contract.Requires(revisionId != Guid.Empty);
			Contract.Requires(events != null);

			RevisionId = revisionId;
			_events = events;
		}

		public AggregateVersion(AggregateVersion baseVersion, Guid revisionId, IEnumerable<Event> events) : this(revisionId, events)
		{
			BaseVersion = baseVersion;
		}

		public AggregateVersion BaseVersion { get { return GetValue(BaseVersionField); } private set { SetValue(BaseVersionField, value); } }
		public Guid RevisionId { get { return GetValue(RevisionIdField); } private set { SetValue(RevisionIdField, value); } }
		
		public IEnumerator<Event> GetEnumerator()
		{
			return GetEvents(this).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private IEnumerable<Event> GetEvents(AggregateVersion version)
		{
			if(version.BaseVersion != null)
			{
				foreach(var baseEvent in GetEvents(version.BaseVersion))
				{
					yield return baseEvent;
				}
			}

			foreach(var @event in version._events)
			{
				yield return @event;
			}
		}
	}
}