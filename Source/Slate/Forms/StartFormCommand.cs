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
	public class StartFormCommand : Command
	{
		public static readonly Field<EntityId> WorkItemIdField = Field.On<StartFormCommand>.For(x => x.WorkItemId);
		public static readonly Field<EntityId> FormIdField = Field.On<StartFormCommand>.For(x => x.FormId);
		public static readonly Field<string> NameField = Field.On<StartFormCommand>.For(x => x.Name);

		public StartFormCommand(EntityId workItemId, EntityId formId, string name)
		{
			Contract.Requires(workItemId != EntityId.Unassigned);
			Contract.Requires(formId != EntityId.Unassigned);
			Contract.Requires(name != null);

			WorkItemId = workItemId;
			FormId = formId;
			Name = name;
		}

		public EntityId WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public EntityId FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}