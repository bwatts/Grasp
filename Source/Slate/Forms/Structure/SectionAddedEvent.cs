using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms.Structure
{
	public class SectionAddedEvent : Event
	{
		public static readonly Field<EntityId> FormIdField = Field.On<AddSectionCommand>.For(x => x.FormId);
		public static readonly Field<EntityId> ParentSectionIdField = Field.On<AddSectionCommand>.For(x => x.ParentSectionId);
		public static readonly Field<EntityId> SectionIdField = Field.On<AddSectionCommand>.For(x => x.ParentSectionId);
		public static readonly Field<string> TitleField = Field.On<AddSectionCommand>.For(x => x.Title);

		public SectionAddedEvent(EntityId formId, EntityId parentSectionId, EntityId sectionId, string title)
		{
			Contract.Requires(formId != EntityId.Unassigned);
			Contract.Requires(parentSectionId != EntityId.Unassigned);
			Contract.Requires(sectionId != EntityId.Unassigned);
			Contract.Requires(title != null);

			FormId = formId;
			ParentSectionId = parentSectionId;
			SectionId = sectionId;
			Title = title;
		}

		public EntityId FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
		public EntityId ParentSectionId { get { return GetValue(ParentSectionIdField); } private set { SetValue(ParentSectionIdField, value); } }
		public EntityId SectionId { get { return GetValue(SectionIdField); } private set { SetValue(SectionIdField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
	}
}