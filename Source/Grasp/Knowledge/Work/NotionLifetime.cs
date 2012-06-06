using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	public class NotionLifetime
	{
		public static readonly Field<NotionLifetimeChange> WhenCreatedField = Field.AttachedTo<Notion>.By<NotionLifetime>.For(x => GetWhenCreated(x));
		public static readonly Field<NotionLifetimeChange> WhenReconstitutedField = Field.AttachedTo<Notion>.By<NotionLifetime>.For(x => GetWhenReconstituted(x));
		public static readonly Field<NotionLifetimeChange> WhenUpdatedField = Field.AttachedTo<Notion>.By<NotionLifetime>.For(x => GetWhenUpdated(x));
		public static readonly Field<NotionLifetimeChange> WhenAddedToSetField = Field.AttachedTo<Notion>.By<NotionLifetime>.For(x => GetWhenAddedToSet(x));
		public static readonly Field<NotionLifetimeChange> WhenRemovedFromSetField = Field.AttachedTo<Notion>.By<NotionLifetime>.For(x => GetWhenRemovedFromSet(x));
		
		public static NotionLifetimeChange GetWhenCreated(Notion notion)
		{
			return notion.GetValue(WhenCreatedField);
		}

		public static void SetWhenCreated(Notion notion, NotionLifetimeChange value)
		{
			notion.SetValue(WhenCreatedField, value);
		}

		public static NotionLifetimeChange GetWhenReconstituted(Notion notion)
		{
			return notion.GetValue(WhenReconstitutedField);
		}

		public static void SetWhenReconstituted(Notion notion, NotionLifetimeChange value)
		{
			notion.SetValue(WhenReconstitutedField, value);
		}

		public static NotionLifetimeChange GetWhenUpdated(Notion notion)
		{
			return notion.GetValue(WhenUpdatedField);
		}

		public static void SetWhenUpdated(Notion notion, NotionLifetimeChange value)
		{
			notion.SetValue(WhenUpdatedField, value);
		}

		public static NotionLifetimeChange GetWhenAddedToSet(Notion notion)
		{
			return notion.GetValue(WhenAddedToSetField);
		}

		public static void SetWhenAddedToSet(Notion notion, NotionLifetimeChange value)
		{
			notion.SetValue(WhenAddedToSetField, value);
		}

		public static NotionLifetimeChange GetWhenRemovedFromSet(Notion notion)
		{
			return notion.GetValue(WhenRemovedFromSetField);
		}

		public static void SetWhenRemovedFromSet(Notion notion, NotionLifetimeChange value)
		{
			notion.SetValue(WhenRemovedFromSetField, value);
		}
	}
}