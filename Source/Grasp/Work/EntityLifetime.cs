using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Work
{
	public static class EntityLifetime
	{
		public static readonly Field<DateTime> WhenCreatedField = Field.AttachedTo<Notion>.By.Static(typeof(EntityLifetime)).For(() => WhenCreatedField);
		public static readonly Field<DateTime> WhenModifiedField = Field.AttachedTo<Notion>.By.Static(typeof(EntityLifetime)).For(() => WhenModifiedField);
		public static readonly Field<DateTime?> WhenReconstitutedField = Field.AttachedTo<Notion>.By.Static(typeof(EntityLifetime)).For(() => WhenReconstitutedField);
	}
}