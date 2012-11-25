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
	public class AddSectionCommand : Command
	{
		public static readonly Field<EntityId> FormIdField = Field.On<AddSectionCommand>.For(x => x.FormId);
		public static readonly Field<EntityId> ParentSectionIdField = Field.On<AddSectionCommand>.For(x => x.ParentSectionId);
		public static readonly Field<string> TitleField = Field.On<AddSectionCommand>.For(x => x.Title);

		public AddSectionCommand(EntityId formId, EntityId parentSectionId, string title)
		{
			Contract.Requires(formId != EntityId.Unassigned);
			Contract.Requires(parentSectionId != EntityId.Unassigned);
			Contract.Requires(title != null);

			FormId = formId;
			ParentSectionId = parentSectionId;
			Title = title;
		}

		public EntityId FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
		public EntityId ParentSectionId { get { return GetValue(ParentSectionIdField); } private set { SetValue(ParentSectionIdField, value); } }
		public string Title { get { return GetValue(TitleField); } private set { SetValue(TitleField, value); } }
	}
}