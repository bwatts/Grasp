using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Grasp.Messaging;

namespace Grasp.Work
{
	public class Timeline : Notion
	{
		public static readonly Field<ManyInOrder<Revision>> RevisionsField = Field.On<Timeline>.For(x => x.Revisions);
		public static readonly Field<Revision> LastRevisionField = Field.On<Timeline>.For(x => x.LastRevision);
		public static readonly Field<Revision> NextRevisionField = Field.On<Timeline>.For(x => x.NextRevision);

		public Timeline(IEnumerable<Revision> revisions = null)
		{
			Revisions = (revisions ?? Enumerable.Empty<Revision>())
				.OrderBy(revision => Lifetime.CreatedEventField.Get(revision))
				.ToManyInOrder();

			LastRevision = Revisions.LastOrDefault();

			NextRevision = new Revision();
		}

		public ManyInOrder<Revision> Revisions { get { return GetValue(RevisionsField); } private set { SetValue(RevisionsField, value); } }
		public Revision LastRevision { get { return GetValue(LastRevisionField); } private set { SetValue(LastRevisionField, value); } }
		public Revision NextRevision { get { return GetValue(NextRevisionField); } private set { SetValue(NextRevisionField, value); } }

		public void ApplyEvents(Action<Event> apply)
		{
			Contract.Requires(apply != null);

			foreach(var revision in Revisions.Concat(Params.Of(NextRevision)))
			{
				revision.ApplyEvents(apply);
			}
		}

		public void ObserveEvent(Event e)
		{
			NextRevision.ObserveEvent(e);
		}

		public Revision RetrieveChanges()
		{
			try
			{
				return NextRevision;
			}
			finally
			{
				NextRevision = new Revision();
			}
		}
	}
}