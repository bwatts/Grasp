using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	public sealed class NotionLifetime
	{
		public static readonly Field<NotionLifetimeChange> WhenCreatedField = Field.AttachedTo<Notion>.By<NotionLifetime>.Backing(() => WhenCreatedField);

		public static readonly Field<NotionLifetimeChange> WhenModifiedField = Field.AttachedTo<Notion>.By<NotionLifetime>.Backing(() => WhenModifiedField);

		public static readonly Field<NotionLifetimeChange> WhenReconstitutedField = Field.AttachedTo<Notion>.By<NotionLifetime>.Backing(() => WhenReconstitutedField);

		public static readonly Field<NotionLifetimeChange> WhenAddedToSetField = Field.AttachedTo<Notion>.By<NotionLifetime>.Backing(() => WhenAddedToSetField);

		public static readonly Field<NotionLifetimeChange> WhenRemovedFromSetField = Field.AttachedTo<Notion>.By<NotionLifetime>.Backing(() => WhenRemovedFromSetField);
	}
}