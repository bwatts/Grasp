using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Cloak.Time;

namespace Grasp
{
	public static class Lifetime
	{
		public static readonly Field<DateTime> WhenCreatedField = Field.AttachedTo<Notion>.By.Static(typeof(Lifetime)).For(() => WhenCreatedField);
		public static readonly Field<DateTime> WhenModifiedField = Field.AttachedTo<Notion>.By.Static(typeof(Lifetime)).For(() => WhenModifiedField);
		public static readonly Field<DateTime?> WhenReconstitutedField = Field.AttachedTo<Notion>.By.Static(typeof(Lifetime)).For(() => WhenReconstitutedField);
		public static readonly Field<ITimeContext> TimeContextField = Field.AttachedTo<Notion>.By.Static(typeof(Lifetime)).For(() => TimeContextField);

		public static DateTime Now(this Notion notion)
		{
			Contract.Requires(notion != null);

			return notion.ResolveTimeContext().Now;
		}

		public static string NowRelative(this Notion notion)
		{
			Contract.Requires(notion != null);

			var timeContext = notion.ResolveTimeContext();

			return timeContext.GetRelativeText(timeContext.Now);
		}

		private static ITimeContext ResolveTimeContext(this Notion notion)
		{
			return TimeContextField.Get(notion) ?? AmbientTimeContext.Resolve();
		}
	}
}