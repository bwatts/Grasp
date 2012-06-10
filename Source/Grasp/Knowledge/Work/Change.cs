using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Knowledge.Work
{
	public abstract class Change : Notion
	{
		public static readonly Field<ChangeType> TypeField = Field.On<Change>.Backing(x => x.Type);
		public static readonly Field<Notion> NotionField = Field.On<Change>.Backing(x => x.Notion);
		public static readonly Field<DateTime> WhenField = Field.On<Change>.Backing(x => x.When);

		protected Change(ChangeType type)
		{
			Type = type;
		}

		public ChangeType Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }
		public Notion Notion { get { return GetValue(NotionField); } }
		public DateTime When { get { return GetValue(WhenField); } }

		public static NotionLifetimeChange EntityCreated(Notion notion, DateTime when)
		{
			return NotionLifetime(ChangeType.EntityCreated, notion, when);
		}

		public static NotionLifetimeChange EntityModified(Notion notion, DateTime when)
		{
			return NotionLifetime(ChangeType.EntityModified, notion, when);
		}

		public static NotionLifetimeChange EntityReconstitued(Notion notion, DateTime when)
		{
			return NotionLifetime(ChangeType.EntityReconstituted, notion, when);
		}

		public static NotionLifetimeChange EntityDeleted(Notion notion, DateTime when)
		{
			return NotionLifetime(ChangeType.EntityDeleted, notion, when);
		}

		public static EntitySetChange EntityAddedToSet(Notion notion, DateTime when, IEntitySet entitySet)
		{
			return EntitySet(ChangeType.EntityAddedToSet, notion, when, entitySet);
		}

		public static EntitySetChange EntityRemovedFromSet(Notion notion, DateTime when, IEntitySet entitySet)
		{
			return EntitySet(ChangeType.EntityRemovedFromSet, notion, when, entitySet);
		}

		public static FieldChange FieldUpdated(Notion notion, DateTime when, Field field, object oldValue, object newValue)
		{
			var change = new FieldChange();

			SetBaseValues(change, notion, when);

			change.SetValue(FieldChange.FieldField, field);
			change.SetValue(FieldChange.OldValueField, oldValue);
			change.SetValue(FieldChange.NewValueField, newValue);

			return change;
		}

		internal static EntitySetChange EntitySet(ChangeType type, Notion notion, DateTime when, IEntitySet entitySet)
		{
			var change = new EntitySetChange(type);

			SetBaseValues(change, notion, when);

			change.SetValue(EntitySetChange.EntitySetField, entitySet);

			return change;
		}

		internal static NotionLifetimeChange NotionLifetime(ChangeType type, Notion notion, DateTime when)
		{
			Contract.Requires(notion != null);

			var change = new NotionLifetimeChange(type);

			SetBaseValues(change, notion, when);

			return change;
		}

		internal static void SetBaseValues(Change change, Notion notion, DateTime when)
		{
			change.SetValue(Change.NotionField, notion);
			change.SetValue(Change.WhenField, when);
		}
	}
}