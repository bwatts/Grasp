using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;
using Grasp.Work.Persistence;

namespace Grasp.Raven
{
	/// <summary>
	/// A Raven document representing a related set of events on an aggregate's timeline
	/// </summary>
	public class Revision : PersistentNotion<Guid>
	{
		public static readonly Field<string> AggregateIdField = Field.On<Revision>.For(x => x.AggregateId);
		public static readonly Field<Guid?> BaseRevisionIdField = Field.On<Revision>.For(x => x.BaseRevisionId);
		public static readonly Field<int> NumberField = Field.On<Revision>.For(x => x.Number);
		public static readonly Field<ManyInOrder<Event>> EventsField = Field.On<Revision>.For(x => x.Events);

		public Revision(string aggregateId, Guid? baseRevisionId, int number, IEnumerable<Event> events) : base(Guid.NewGuid())
		{
			Contract.Requires(!String.IsNullOrEmpty(aggregateId));
			Contract.Requires(baseRevisionId != Guid.Empty);
			Contract.Requires(number > 0);
			Contract.Requires(events != null);

			AggregateId = aggregateId;
			BaseRevisionId = baseRevisionId;
			Number = number;
			Events = events.ToManyInOrder();
		}

		public string AggregateId { get { return GetValue(AggregateIdField); } private set { SetValue(AggregateIdField, value); } }
		public Guid? BaseRevisionId { get { return GetValue(BaseRevisionIdField); } private set { SetValue(BaseRevisionIdField, value); } }
		public int Number { get { return GetValue(NumberField); } private set { SetValue(NumberField, value); } }
		public ManyInOrder<Event> Events { get { return GetValue(EventsField); } private set { SetValue(EventsField, value); } }
	}
}