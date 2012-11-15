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
		public static readonly Field<Guid> WorkItemIdField = Field.On<StartFormCommand>.For(x => x.WorkItemId);
		public static readonly Field<Guid> FormIdField = Field.On<StartFormCommand>.For(x => x.FormId);
		public static readonly Field<string> NameField = Field.On<StartFormCommand>.For(x => x.Name);

		public StartFormCommand(Guid workItemId, Guid formId, string name)
		{
			Contract.Requires(workItemId != Guid.Empty);
			Contract.Requires(formId != Guid.Empty);
			Contract.Requires(name != null);

			WorkItemId = workItemId;
			FormId = formId;
			Name = name;
		}

		public Guid WorkItemId { get { return GetValue(WorkItemIdField); } private set { SetValue(WorkItemIdField, value); } }
		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
		public string Name { get { return GetValue(NameField); } private set { SetValue(NameField, value); } }
	}
}