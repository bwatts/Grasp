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
	public class StartTestingCommand : Command
	{
		public static readonly Field<EntityId> FormIdField = Field.On<StartTestingCommand>.For(x => x.FormId);

		public StartTestingCommand(EntityId formId)
		{
			Contract.Requires(formId != EntityId.Unassigned);

			FormId = formId;
		}

		public EntityId FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}