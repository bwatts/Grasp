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
		public static readonly Field<Guid> FormIdField = Field.On<StartTestingCommand>.For(x => x.FormId);

		public StartTestingCommand(Guid formId)
		{
			Contract.Requires(formId != Guid.Empty);

			FormId = formId;
		}

		public Guid FormId { get { return GetValue(FormIdField); } private set { SetValue(FormIdField, value); } }
	}
}