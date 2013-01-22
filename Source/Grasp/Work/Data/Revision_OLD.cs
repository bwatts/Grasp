using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work
{
	public class Revision : Notion
	{
		public static readonly Field<RevisionId> IdField = Field.On<Revision>.For(x => x.Id);
		public static readonly Field<ManyInOrder<Event>> EventsField = Field.On<Revision>.For(x => x.Events);

		public Revision()
		{
			Id = RevisionId.Disconnected;
			Events = new ManyInOrder<Event>();
		}

		public RevisionId Id { get { return GetValue(IdField); } private set { SetValue(IdField, value); } }
		public ManyInOrder<Event> Events { get { return GetValue(EventsField); } private set { SetValue(EventsField, value); } }

		public void ObserveEvent(Event e)
		{
			Contract.Requires(e != null);

			Events.AsWriteable().Add(e);
		}

		public void ApplyEvents(Action<Event> apply)
		{
			Contract.Requires(apply != null);

			foreach(var e in Events)
			{
				apply(e);
			}
		}
	}
}