using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak.Time;
using Grasp.Messaging;

namespace Grasp
{
	public static class Lifetime
	{
		public static readonly Trait EventsTrait = Trait.DeclaredBy(typeof(Lifetime));
		public static readonly Trait TimeContextTrait = Trait.DeclaredBy(typeof(Lifetime));

		public static readonly Field<Event> CreatedEventField = EventsTrait.Field(() => CreatedEventField);
		public static readonly Field<Event> ModifiedEventField = EventsTrait.Field(() => ModifiedEventField);
		public static readonly Field<Event> ReconstitutedEventField = EventsTrait.Field(() => ReconstitutedEventField);

		public static readonly Field<ITimeContext> TimeContextField = TimeContextTrait.Field(() => TimeContextField);

		public static DateTime Now(this Notion notion)
		{
			Contract.Requires(notion != null);

			return ResolveTimeContext(notion).Now;
		}

		public static RelativeTime NowRelativeTo(this Notion notion, DateTime referencePoint)
		{
			Contract.Requires(notion != null);

			return ResolveTimeContext(notion).NowRelativeTo(referencePoint);
		}

		private static ITimeContext ResolveTimeContext(Notion notion)
		{
			return TimeContextField.Get(notion) ?? AmbientTimeContext.Resolve();
		}
	}
}