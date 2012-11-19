using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp;
using Grasp.Messaging;

namespace Slate.Forms
{
	public class FormStartedEvent : Event
	{
		public static readonly Field<EntityId> WorkItemIdField = Field.On<FormStartedEvent>.For(x => x.WorkItemId);
		public static readonly Field<EntityId> FormIdField = Field.On<FormStartedEvent>.For(x => x.FormId);
		public static readonly Field<string> NameField = Field.On<FormStartedEvent>.For(x => x.Name);

		public FormStartedEvent(EntityId workItemId, EntityId formId, string name)
		{
			WorkItemId = workItemId;
			FormId = formId;
			Name = name;
		}

		public EntityId WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public EntityId FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}