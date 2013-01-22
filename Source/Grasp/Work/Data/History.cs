using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Work.Data
{
	public abstract class History : Notion
	{
		public static readonly Field<Timeline> TimelineField = Field.On<History>.For(x => x.Timeline);

		protected History(Timeline timeline)
		{
			Contract.Requires(timeline != null);

			Timeline = timeline;
		}

		public Timeline Timeline { get { return GetValue(TimelineField); } private set { SetValue(TimelineField, value); } }

		public abstract Task<List<Event>> GetEventsAsync(Func<IQueryable<Event>, IQueryable<Event>> query = null);
	}
}